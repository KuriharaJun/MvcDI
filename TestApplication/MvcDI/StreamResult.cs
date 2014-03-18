using System.IO;
using System.Text;
using System.Web.Mvc;

namespace MvcDI
{
    public class StreamResult : FileResult
    {
        /// <summary>
        /// 出力ファイルエンコード
        /// </summary>
        private Encoding _encode = Encoding.GetEncoding("Shift_JIS");

        /// <summary>
        /// 書込データ
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 書込バイトデータ
        /// </summary>
        public byte[] ByteData { get; set; }

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mime"></param>
        public StreamResult(string mime, string fileName)
            : base(mime)
        {
            base.FileDownloadName = fileName;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mime"></param>
        /// <param name="data"></param>
        public StreamResult(string mime, string fileName, string data)
            : this(mime, fileName)
        {
            this.Data = data;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mime"></param>
        /// <param name="data"></param>
        public StreamResult(string mime, string fileName, byte[] data)
            : this(mime, fileName)
        {
            this.ByteData = data;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mime"></param>
        /// <param name="encode"></param>
        public StreamResult(string mime, string fileName, Encoding encode)
            : this(mime, fileName)
        {
            this._encode = encode;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mime"></param>
        /// <param name="encode"></param>
        /// <param name="data"></param>
        public StreamResult(string mime, string fileName, Encoding encode, string data)
            : this(mime, fileName, encode)
        {
            this.Data = data;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mime"></param>
        /// <param name="encode"></param>
        /// <param name="data"></param>
        public StreamResult(string mime, string fileName, Encoding encode, byte[] data)
            : this(mime, fileName, encode)
        {
            this.ByteData = data;
        }
        #endregion

        protected override void WriteFile(System.Web.HttpResponseBase response)
        {
            response.ContentType = base.ContentType;
            Stream outputStrream = response.OutputStream;
            using (var memStream = new MemoryStream())
            {
                if (string.IsNullOrEmpty(this.Data) == true)
                {
                    // バイナリファイルの出力
                    using (BinaryWriter writer = new BinaryWriter(memStream))
                    {
                        writer.Write(this.ByteData, 0, this.ByteData.Length);
                        writer.Flush();
                        outputStrream.Write(memStream.GetBuffer(), 0, (int)memStream.Length);
                    }
                }
                else
                {
                    // テキストファイルの出力
                    using (var writer = new StreamWriter(memStream, this._encode))
                    {
                        writer.Write(this.Data);
                        writer.Flush();
                        response.ContentEncoding = this._encode;
                        outputStrream.Write(memStream.GetBuffer(), 0, (int)memStream.Length);
                    }
                }
            }
        }
    }
}
