/*
 *  Class DBObjectFactory
 * 
 *   
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

public abstract class DBObjectFactory<T> : DBObjectInterface
	where T : DBObjectFactory<T>, new()
{

	/*
	 *  Use this for initialization from database
	 * 	input: a row of database.
	 *  out: DBObjectFactory
	 * */	 
	public static T create(DataRow dr)
	{
		T m_T = new T();
		m_T.Initialize(dr);
		return m_T;
	}
	
	/*
	 *  Use this for initialization from Xml
	 * 	input: a xml note.
	 *  out: DBObjectFactory
	 * */
	public static T API_create(XmlNode nodeItem)
	{
		T m_T = new T();
		m_T.Initialize(nodeItem);
		return m_T;
	}
	
	/*
	 *  Use this for load list DBObjectFactory from database
	 * 	input: connector, database name, table name, query condition, connection flag.
	 *  out: list DBObjectFactory
	 * */
	public static List<T> load_list(
		ILocalData data_loader, 
		string db_name, 
		string table_name,
		string condition, //"id=89"
		bool open_connection)
	{
		List<T> list = new List<T>();
		
		// if connection flag is true then open database
//		if(open_connection)
//			data_loader.myDB.ConnectWithDB(db_name);
		
		// create a query
		string sqL_cmd = string.Format("SELECT * FROM {0} WHERE {1}", 
		table_name, condition);
	
		// print out to console
		try {
			// excute query
			DataTable temp = new DataTable();

			temp = data_loader.excuteQuery(sqL_cmd);

			// print out to console
			
			foreach(DataRow dr in temp.Rows)
			{
				// create a row
				T data = new T();
				// Initialize a row
				data.Initialize(dr);
				// add a row to list
				list.Add(data);
	        }
		}
		catch(System.Exception e)
		{
			// print out to console
			Debug.Log (e.ToString());
		}
		
		// if connection flag is true then close database
//		if(open_connection)
//			data_loader.myDB.DisconnectWithDB();
		
		return list;
	}
	
	/*
	 *  Use this for initialization from Xml
	 * 	input: a xml note.
	 *  out: DBObjectFactory
	 * */
	public virtual void load(
		ILocalData data_loader, 
		string db_name, 
		string table_name,
		string condition, 
		bool open_connection)
	{		
		if(data_loader != null)
		{
//			if(open_connection)
//				data_loader.myDB.ConnectWithDB(db_name);
			
			string sqL_cmd = string.Format("SELECT * FROM {0} WHERE {1}", 
			table_name, condition);
			
//			Debug.Log ("DBObject.load:" + sqL_cmd);
		
			try {
				// excute query
				DataTable temp = new DataTable();
					
				temp = data_loader.excuteQuery(sqL_cmd);

				
//				Debug.Log ("number rows:" + temp.Rows.Count);
				
				if(temp.Rows.Count >= 1)
				{
					Initialize(temp.Rows[0]);
		        }
			}
			catch(System.Exception e)
			{
				Debug.Log (e.ToString());
			}
			
//			if(open_connection)
//				data_loader.myDB.DisconnectWithDB();
		}
	}

	public static Hashtable Hash(params object[] args){
		Hashtable hashTable = new Hashtable(args.Length/2);
		if (args.Length %2 != 0) {
			Debug.LogError("Tween Error: Hash requires an even number of arguments!"); 
			return null;
		}else{
			int i = 0;
			while(i < args.Length - 1) {
				hashTable.Add(args[i], args[i+1]);
				i += 2;
			}
			return hashTable;
		}
	}	

	public virtual bool update(params object[] args)
	{
		if (args.Length %2 != 0 && args.Length < 2) {
			Debug.LogError("update query need at least one parameter"); 
			return false;
		}

		string nameTable = this.GetType().BaseType.Name.ToLower();
		if(nameTable.Contains("dbobjectfactory")){
			nameTable  = this.GetType().Name.ToLower();
		}

		StringBuilder sqlBuilder = new StringBuilder();
		sqlBuilder.AppendFormat("UPDATE {0} SET ", nameTable);

		if(args[1].GetType() == typeof(string))
			sqlBuilder.AppendFormat("{0}='{1}'", args[0], args[1]);
		else
			sqlBuilder.AppendFormat("{0}={1}", args[0], args[1]);

		int i = 2;
		while(i < args.Length - 1) 
		{
			if(args[i+1].GetType() == typeof(string))
				sqlBuilder.AppendFormat(", {0}='{1}'", args[i], args[i+1]);
			else
				sqlBuilder.AppendFormat(", {0}={1}", args[i], args[i+1]);
			i += 2;
		}

		Debug.Log ("update " + nameTable + " sql: " + sqlBuilder.ToString());

		return ILocalData.UserDBQuery().excuteNonQuery(sqlBuilder.ToString());
	}

	public virtual bool update()
	{		
		return ILocalData.UserDBQuery().updateObject(this);
	}
	
	public virtual bool delete()
	{	
		return ILocalData.UserDBQuery().deleteObject(this);
	}
	
	public virtual bool insert()
	{		
		return ILocalData.UserDBQuery().insertObject(this);
	}
	
	public virtual void Initialize(DataRow dr)
	{
		foreach(PropertyInfo prop in GetType().GetProperties() ) 
		{
			object property_value = dr[prop.Name];
			
			if(property_value == null)
				continue;
			
			bool flag = false;
			
			foreach (Attribute attr in
                Attribute.GetCustomAttributes(prop)) 
			{
                if (attr.GetType() == typeof(UsableProperty))
				{
					flag = true;
					break;					
				}
            }
			
			if(!flag)
				continue;

			prop.SetValue((System.Object)this, Convert.ChangeType(property_value, prop.PropertyType), null );

		}	
	}
	
	public virtual void Initialize(XmlNode nodeItem)
	{
		foreach(PropertyInfo prop in GetType().GetProperties() ) 
		{
            string property_name = string.Format("descendant::{0}", prop.Name);
//            Debug.Log("SelectSingleNode withPropertyName: " + property_name);

            XmlNode node = nodeItem.SelectSingleNode( property_name );
			if(node == null)
				continue;
						
			bool flag = false;
			
			foreach (Attribute attr in
                Attribute.GetCustomAttributes(prop)) 
			{
                if (attr.GetType() == typeof(UsableProperty))
				{
					flag = true;
					break;					
				}
            }
			
			if(!flag)
				continue;
			
			if(!string.IsNullOrEmpty(node.InnerText)){
				prop.SetValue((System.Object)this, Convert.ChangeType(node.InnerText, prop.PropertyType), null );
			}
		}	
	}
	
}


public class UsableProperty : Attribute {
}

public class NotQueryProperty : Attribute {
}

public interface DBObjectInterface {
	
	void load(
		ILocalData data_loader, 
		string db_name, 
		string table_name,
		string condition, 
		bool open_connection);
	
	bool insert();
	
	bool delete();
	
	bool update();
	
	void Initialize(DataRow dr);
	
	void Initialize(XmlNode nodeItem);
}