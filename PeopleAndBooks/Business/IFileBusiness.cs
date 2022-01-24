using Microsoft.AspNetCore.Http;
using PeopleAndBooks.DataConverter.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeopleAndBooks.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveManyFileToDisk(IList<IFormFile> file);
    }
}
