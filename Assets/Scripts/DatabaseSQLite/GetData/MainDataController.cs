using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MainDataController
{

	public static List<USERS> GetUserListSort()
	{
		List<USERS> listUserSort = new List<USERS>();
		string sql = "SELECT * FROM user  ORDER BY id DESC ";
		DataTable table = ILocalData.MasterDBQuery().excuteQuery(sql);	

		if (table != null && table.Rows.Count > 0)
		{
			for (int i = 0; i < table.Rows.Count; i++)
			{
				USERS newResult = new USERS();
				newResult.Initialize(table.Rows [i]);
				listUserSort.Add(newResult);
			}
		}	
		return listUserSort;
	}

	public static void UpdateUserName(string key, object value,int id)
	{  //update  user set username = 'pham thanh dat' where id = 3;
		string sql;
		if(value.GetType() == typeof(string))
			sql = string.Format( "UPDATE user SET {0}='{1}' WHERE id={2}", key, value ,id);
		else
			sql = string.Format( "UPDATE user SET {0}={1} WHERE id={2}", key, value ,id);
		try 
		{
			ILocalData.UserDBQuery().excuteNonQuery(sql);
		}
		catch(System.Exception e)
		{
			Debug.Log (e.ToString());
		}
	}

    
    public static List<USERS> GetUserList()
    {
		List<USERS> userList;
		userList = ILocalData.UserDBQuery().GetTable<USERS>(USERS.tableName, 
		                                                    ILocalData.UserDBQuery().NameDB);
		return userList;
    }


}
