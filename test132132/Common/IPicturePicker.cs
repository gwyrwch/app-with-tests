using System;
using System.IO;
using System.Threading.Tasks;

namespace test132132.Common
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }

}
