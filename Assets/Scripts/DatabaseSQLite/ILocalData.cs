using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ILocalData 
{
	static string MasterDBName = "data00";
	static string UserDBName = "datasqlite";

	static ILocalData masterDBQuery = null;

	public void saveDB (byte[] data)
	{
		string path = "";

//		if(!System.IO.Directory.Exists(path))
//			System.IO.Directory.CreateDirectory(path);
//		Debug.Log ("path " + path);
//		Debug.Log ("nameDB " + nameDB);
//		Debug.Log ("data " + data.ToString());
		FileUtilities.SaveFile (path, nameDB, data);
	}

    public void cloneDB()
    {
        string sourcePath = System.IO.Path.Combine (Application.streamingAssetsPath, nameDB);
        string desPath = Path.Combine(Application.persistentDataPath, nameDB);
        File.Copy(sourcePath, desPath, true);
    }

	public static ILocalData MasterDBQuery()
	{
		if(masterDBQuery == null)
			masterDBQuery = new ILocalData(MasterDBName);

		return masterDBQuery;
	}

	static ILocalData userDBQuery = null;

	public static ILocalData UserDBQuery()
	{
		if(userDBQuery == null)
			userDBQuery = new ILocalData(UserDBName);

		return userDBQuery;
	}

	string nameDB;

	public string NameDB 
	{
		get { return nameDB; }
	}

	iDBQuery dbQuery = null;

	public ILocalData (string dbName)
	{
		nameDB = dbName;		
		initDBQuery();
	}
	
	void initDBQuery()
	{
		dbQuery = new iDBQuery();
	}

	public DataTable excuteQuery(string sql_cmd)
	{
		dbQuery.ConnectWithDB(nameDB);
		DataTable table = dbQuery.excuteQuery(sql_cmd);
		dbQuery.DisconnectWithDB();
		return table;
	}

	public bool excuteNonQuery(string sql_cmd)
	{
		dbQuery.ConnectWithDB(nameDB);
		bool result = dbQuery.excuteString(sql_cmd);
		dbQuery.DisconnectWithDB();
		return result;
	}

	public bool updateObject(object obj, bool open_connection = true, string table_name = "")
	{
		bool result = false;
		dbQuery.ConnectWithDB(nameDB);
		result = dbQuery.excuteUpdateObject(obj, table_name);
		dbQuery.DisconnectWithDB();
		return result;
	}	
	
	public bool deleteObject(object obj, bool open_connection = true)
	{				
		bool result = false;
		dbQuery.ConnectWithDB(nameDB);
		result = dbQuery.excuteDeleteObject(obj);
		dbQuery.DisconnectWithDB();
		return result;
	}
	
	public bool insertObject(object obj, bool open_connection = true, string table_name = "")
	{				
		bool result = false;
		dbQuery.ConnectWithDB(nameDB);
		result = dbQuery.excuteInsertObject(obj, table_name);
		dbQuery.DisconnectWithDB();
		return result;
	}

    
    public List<T> GetTable<T>(string table_name, string nameDataBase) where T : DBObjectInterface, new()
    {
        List<T> list = new List<T>();
        
        //      if(open_connection)
        //          myDB.ConnectWithDB(nameDB);
        
        string sqL_cmd = string.Format("SELECT * FROM {0} ", 
                                       table_name);
        //      Debug.Log(sqL_cmd);
        DataTable temp = excuteQuery(sqL_cmd);
        
        foreach(DataRow dr in temp.Rows)
        {
            T data = new T();
            //DBObjectFactory<T>.create(dr);
            data.Initialize(dr);
            list.Add (data);
        }
        
        //      if(open_connection)
        //          myDB.DisconnectWithDB();
        
        return list;
    }


    public List<T> GetTable<T>(string table_name, string condition, string nameDataBase) where T : DBObjectInterface, new()
    {
        List<T> list = new List<T>();
        
        //      if(open_connection)
        //          myDB.ConnectWithDB(nameDB);
        
        string sqL_cmd = string.Format("SELECT * FROM {0} WHERE {1}", 
                                       table_name, condition);
        //Debug.Log(sqL_cmd);
        
        DataTable temp = excuteQuery(sqL_cmd);
        
        foreach(DataRow dr in temp.Rows)
        {
            T data = new T();
            //DBObjectFactory<T>.create(dr);
            data.Initialize(dr);
            list.Add (data);
        }
        
        //      if(open_connection)
        //          myDB.DisconnectWithDB();
        
        return list;
    }
    
    public T GetTable<T>(string table_name, string key, object value, string nameDataBase) where T : DBObjectFactory<T>, new()
    {
        T data = default(T);
        
        //      if(open_connection)
        //          myDB.ConnectWithDB(nameDB);
        
        string condition;
        
        if(value.GetType() == typeof(string))
            condition = string.Format("{0}='{1}'", key, value );
        else
            condition = string.Format("{0}={1}", key, value );
        
        string sqL_cmd = string.Format("SELECT * FROM {0} WHERE {1}", 
                                       table_name, condition);
        
        //      DevLog.Log (sqL_cmd);
        DataTable temp = excuteQuery(sqL_cmd);
        
        //DevLog.Log ("temp.Rows.Count = " + temp.Rows.Count);
        if(temp.Rows.Count == 1)
        {
            data = DBObjectFactory<T>.create(temp.Rows[0]);
        }
        else
            data = null;
        //DevLog.Log ("data=" + data);
        
        //      if(open_connection)
        //          myDB.DisconnectWithDB();
        
        return data;
    }

}
