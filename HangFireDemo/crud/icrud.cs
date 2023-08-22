using Hangfire.Logging;
using System.Data;
using HangFireDemo.Models;

namespace HangFireDemo.crud
{
    public interface icrud
    {
        void SendEmail();
        void InsertRecords(Diary diary);

        DataTable SyncData();

        List<Diary> GetAllRecords();
        bool DeleteRecords(int ID);

    }
}
