using CarbonlessWebOperations.Classes.Events;
using CarbonlessWebOperations.Classes.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using CarbonlessWebOperations.Classes.HelperObjects;
using System.Net;
using static CarbonlessWebOperations.Classes.Events.EventHandlers;
using Syroot.Windows.IO;
using System.IO;
using static CarbonlessWebOperations.Classes.Enums.Enums;
using System.Windows;

namespace CarbonlessWebOperations.Main
{
    public class WebOperations : IOperations
    {
        public event AsyncWebOperationsCompleteEventHandler WebOperationCompleted;
        public event AsyncWebOperationsProgressEventHandler WebOperationProgress;

        /// <summary>
        /// Default constructor of WebOperations.
        /// </summary>
        public WebOperations()
        {
            FileDetail = new WebOperationFile();
            Preferences = new DownloadPreferences();
        }
        /// <summary>
        /// The webrequest object
        /// </summary>
        public WebRequest WebRequest { get; set; }
        /// <summary>
        /// The state of the download.
        /// </summary>
        public State WebOperationState { get; private set; }
        /// <summary>
        /// The File object which contains the file details.
        /// </summary>
        public WebOperationFile FileDetail { get; set; }
        /// <summary>
        /// Network proxy to use to access this Internet resource.
        /// </summary>
        public IWebProxy Proxy { get; set; }
        /// <summary>
        /// Download preferences set for downloads etc. download path.
        /// </summary>
        public DownloadPreferences Preferences { get; set; }
        /// <summary>
        /// Downloads a document from a specified url.
        /// </summary>
        /// <param name="file">The object which holds the downloading file details.</param>
        public async void DownloadFileAsync(WebOperationDownloadObject file)
        {
            try
            {
                WebRequest = (HttpWebRequest)WebRequest.Create(file.Url);
                WebRequest.Proxy = Proxy;
                WebRequest.Method = WebRequestMethods.Http.Get;
                 
                string filePath = SetFilePath(file);


                using (var response = (HttpWebResponse)await WebRequest.GetResponseAsync())
                {
                    long fileSize = response.ContentLength;
                    byte[] buffer = new byte[fileSize];
                    using (var reader = response.GetResponseStream())
                    {
                        using (var filestream = File.Create(filePath))
                        {
                            WriteFileDetail(filePath, fileSize);
                            int bytesRead;
                            while (reader != null && (bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) != 0)
                            {
                                while (WebOperationState == State.Paused)
                                {
                                    await Task.Delay(1000);
                                }
                                await filestream.WriteAsync(buffer, 0, bytesRead);
                                WebOperationProgress?.Invoke(this, new AsyncWebOperationsProgressArgs(fileSize, filestream.Length));
                                await filestream.FlushAsync();
                            }
                        }
                    }
                }

                WebOperationCompleted?.Invoke(this, new AsyncWebOperationsCompleteArgs());
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.RequestCanceled)
                {
                    WebOperationCompleted(this, new AsyncWebOperationsCompleteArgs(State.Stopped));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.InnerException.Message);
            }
        }

        /// <summary>
        /// Suspends the current download thread
        /// </summary>
        public void PauseDownload()
        {
            WebOperationState = State.Paused;
        }

        /// <summary>
        /// Resumes the current download thread
        /// </summary>
        public void ResumeDownload()
        {
            WebOperationState = State.Started;
        }

        /// <summary>
        /// Terminates the current download thread
        /// </summary>
        public void StopDownload()
        {
            WebOperationState = State.Stopped;
            WebRequest.Abort();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnWebOperationCompleted(AsyncWebOperationsCompleteArgs e)
        {
            WebOperationCompleted?.Invoke(this, e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnWebOperationProgress(AsyncWebOperationsProgressArgs e)
        {
            WebOperationProgress?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="fileSize"></param>
        private void WriteFileDetail(string filepath, long fileSize)
        {
            var fileInfo = new FileInfo(filepath);
            FileDetail.Name = fileInfo.Name;
            FileDetail.Extension = fileInfo.Extension;
            FileDetail.SizeInBytes = fileSize;
            FileDetail.Path = filepath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string SetFilePath(WebOperationDownloadObject file)
        {
            if (!string.IsNullOrWhiteSpace(file.Filename))
                return string.IsNullOrEmpty(Preferences.DownloadPath) ?
                    new KnownFolder(KnownFolderType.Downloads).DefaultPath + "/" + file.Filename + file.Url.Segments.Last().Substring(file.Url.Segments.Last().LastIndexOf('.')) :
                    Preferences.DownloadPath + "/" + file.Filename + file.Url.Segments.Last().Substring(file.Url.Segments.Last().LastIndexOf('.'));
            else
                return string.IsNullOrEmpty(Preferences.DownloadPath) ? new KnownFolder(KnownFolderType.Downloads).DefaultPath + "/" + file.Url.Segments.Last() : Preferences.DownloadPath + "/" + file.Url.Segments.Last();
        }
    }
}
