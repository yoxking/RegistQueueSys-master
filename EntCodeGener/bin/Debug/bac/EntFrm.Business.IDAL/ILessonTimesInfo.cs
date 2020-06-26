using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ILessonTimesInfo
  {
        LessonTimesInfoCollections GetAllRecords();
        LessonTimesInfoCollections GetRecordsByClassNo(string sClassNo);
        LessonTimesInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(LessonTimesInfo info);
        int UpdateRecord(LessonTimesInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        LessonTimesInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

