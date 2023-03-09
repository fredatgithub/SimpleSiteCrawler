using System.IO;
using System.IO.Compression;

namespace SimpleSiteCrawler.Lib.Reader
{
  internal class GZipSitePageReader : AbstractSitePageReader
  {
    private const string EncodingGzip = "gzip";

    public static bool CanAccept(string contentEncoding)
    {
      if (string.IsNullOrEmpty(contentEncoding))
      {
        return false;
      }

      return contentEncoding.ToLower().Contains(EncodingGzip);
    }
      

    public GZipSitePageReader(Stream response) : base(response)
    {
    }

    protected override Stream CreateReadStream()
    {
      return new GZipStream(Response, CompressionMode.Decompress);
    }
  }
}