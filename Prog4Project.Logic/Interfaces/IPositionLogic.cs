using Prog4Project.Models;
using System.Linq;

namespace Prog4Project.Logic
{
    public interface IPositionLogic
    {
        void Create(Position item);
        void Delete(int id);
        Position Read(int id);
        IQueryable<Position> ReadAll();
        void Update(Position item);
    }
}