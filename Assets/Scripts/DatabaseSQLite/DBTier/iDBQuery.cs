
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;
public class iDBQuery: Exception
{
	
	private SqliteDatabase db_iOS ;
	PlatformDevice platform;
		
	public iDBQuery ()
	{		
		platform= new PlatformDevice();
		
		if(Application.platform == RuntimePlatform.Android)
		{			
			platform.KIND=2;			
		}
		else if(Application.platform != RuntimePlatform.IPhonePlayer)
		{
			platform.KIND=1;
		}	
	}	
	
	~iDBQuery() 
	{
   		 Console.WriteLine("Out..");
	}
	
	public void DisconnectWithDB()
	{
		db_iOS.Close();
	}	
	
	public void ConnectWithDB(string str)
	{
		db_iOS = new SqliteDatabase(str);

	}
	
	public void dataNotReal()
	{
		
		/// insert TR Army C01
		
	}
	public DataTable GetListTableName()
	{
		string strSelectAllTable ="SELECT NAME FROM sqlite_master WHERE type = 'table'";
		DataTable dataTable = db_iOS.ExecuteQuery(strSelectAllTable);
//		DevLog.Log(strSelectAllTable);		
		return dataTable;
	}
	
	public bool excuteDropAllTable(string tableName)
	{
		string strSelectAllTable="Drop Table "+tableName;
		bool _dropTable = db_iOS.ExecuteNonQuery(strSelectAllTable);
//		DevLog.Log(strSelectAllTable);
		return _dropTable;
	}
	public bool excuteString(string sqlData)
	{
//		DevLog.Log(sqlData);
		bool bInsert = db_iOS.ExecuteNonQuery(sqlData);	
		
		return bInsert;
	}
	
////////////////////////////////// ******GET DATA TABLE********//////////////////////////////////////////////////////////////////////	

////////////////////////////////// SELECT //////////////////////////////////////////////////////////////////////
	public DataTable excuteQuery(string sql_command)
	{	
//		DevLog.Log(sql_command);	
		DataTable dataTable = db_iOS.ExecuteQuery(sql_command);			
			
		return dataTable;
	}
		
	public DataTable excuteSelectALL(string tableName)
	{
		/// select
		string strSelectAllTable =getSelectALLQuery(tableName);
//		DevLog.Log(strSelectAllTable);		
		DataTable dataTable = db_iOS.ExecuteQuery(strSelectAllTable);			
		
//		DevLog .Log(dataTable.Rows.Count);		
		return dataTable;
	}
	public DataTable excuteSelectWithCondition(object _object ,string tableName,string listTable ,string listCondition)
	{
		
		string strSelectquerycondition =getSelectQueryWithCondition(tableName,listTable,listCondition);		
		DataTable dataTable = db_iOS.ExecuteQuery(strSelectquerycondition);
		
		return dataTable;
	}
	public DataTable excuteSelectWithListCoditionInOneTable(string strTBName ,string listCondition,string lisVariable)
	{
		
		string strSelectquerycondition =getSelectQueryByListCoditionInOneTable(strTBName,listCondition,lisVariable);		
		DataTable dataTable = db_iOS.ExecuteQuery(strSelectquerycondition);
		
		return dataTable;
	}
	public DataTable excuteSelectItemsByUserAndKindItem(string listCondition,string lisVariable)
	{
		string strSelectquerycondition =getSelectQueryByListCoditionDiffNameInManyTable(listCondition,lisVariable);	
//		DevLog.Log(strSelectquerycondition);
		DataTable dataTable = db_iOS.ExecuteQuery(strSelectquerycondition);
		
		return dataTable;
	}
	
	public DataTable excuteJoinForDataWeaponObject(List<object> _arrayListObject,string listCondition,string lisVariable)
	{
		string strSelectquerycondition =getSelectQueryByListCoditionDiffNameInManyTableAndTheSameColumnName(_arrayListObject,listCondition,lisVariable);	
//		DevLog.Log(strSelectquerycondition);		
		DataTable dataTable = db_iOS.ExecuteQuery(strSelectquerycondition);
		return dataTable;
		
	}
	public DataTable excuteLEFTJoinForDataObject(List<object> _arrayListObject,string listCondition,string lisVariable)
	{
		string strSelectquerycondition =getSelectQueryByListCoditionDiffNameInManyTableAndTheSameColumnName_LEFTJOINT(_arrayListObject,listCondition,lisVariable);	
//		DevLog.Log(strSelectquerycondition);		
		DataTable dataTable = db_iOS.ExecuteQuery(strSelectquerycondition);
		return dataTable;
	}
	public DataTable excuteNewTableAndMoreAFlagColumn(string TableGET ,string TableCompare,string columGet , string coditionColumn,string coditionJoint)
	{
		string strSelectquerycondition =getselectQueryEditFlagTwoTable(TableGET,TableCompare,columGet,coditionColumn,coditionJoint);	
//		DevLog.Log(strSelectquerycondition);		
		DataTable dataTable = db_iOS.ExecuteQuery(strSelectquerycondition);
		return dataTable;
	}
//////////////////////////////////INSERT ///////////////////////////////////////////////////////////////////////	
	public bool excuteInsertObject(object _object, string table_name = "")
	{
		string strQueryInsert = this.getQueryInsert(_object, table_name);		
//		DevLog.Log(strQueryInsert);		
		bool success=db_iOS.ExecuteNonQuery(strQueryInsert);
		return success;
	}
//////////////////////////////////DELETE ///////////////////////////////////////////////////////////////////////	
	public bool excuteDeleteObject(object _object, string table_name = ""){		
		string nameTable = _object.GetType().BaseType.Name.ToLower();
		if(nameTable.Contains("dbobjectfactory")){
			nameTable  = _object.GetType().Name.ToLower();
		}
		
		if(!string.IsNullOrEmpty(table_name)){
			nameTable = table_name;
		}
		string strQueryDelete = this.getQueryDeletebyListCodition(nameTable, _object, "", "");	
//		DevLog.Log(strQueryDelete);	
		bool success=db_iOS.ExecuteNonQuery(strQueryDelete);
		return success;
	}
//////////////////////////////////UPDATE ///////////////////////////////////////////////////////////////////////	
	public bool excuteUpdateObject(object _object, string table_name = "")
	{
		string nameTable = _object.GetType().BaseType.Name.ToLower();
		if(nameTable.Contains("dbobjectfactory")){
			nameTable  = _object.GetType().Name.ToLower();
		}
		if(!string.IsNullOrEmpty(table_name)){
			nameTable = table_name;
		}
		string queryUpdate=getQueryUpdate(nameTable, _object, "", "");
//		DevLog.Log(queryUpdate);
		bool success = db_iOS.ExecuteNonQuery(queryUpdate);
		return success;
	}
////////////////////////////////// GET -MAX ///////////////////////////////////////////////////////////////////////	
	public DataTable excuteGetMaxAttribute(string attribute , string table)
	{
		string query=getQueryInsert_MaxAutoCounting(attribute,table);
//		DevLog.Log(query);
		DataTable dataTable = db_iOS.ExecuteQuery(query);
		return dataTable;
		
	}
	public DataTable excuteLeftJointWithCodition(List<object> _arrayListObject,string listTable,string listCondition,string aTable,string ListWhere,string listValueWhere)
	{
		string query=getQueryLEFTJointListTableAndListCodition(_arrayListObject,listTable,listCondition,aTable,ListWhere,listValueWhere);
//		DevLog.Log(query);
		DataTable dataTable = db_iOS.ExecuteQuery(query);
		return dataTable;//dataTable;
	}
	
	
	public DataTable excuteSelectWithCoditionMoreThanInOneTable (string tableName, string listCoditionMore,string listSymbleMore,string listValueMore)
	{
		string query=getSelectQueryOneAttributeCompareTwoAttribute(tableName,listCoditionMore,listSymbleMore,listValueMore);
//		DevLog.Log(query);
		DataTable dataTable = db_iOS.ExecuteQuery(query);
		return dataTable;
	}
	public DataTable excuteSelectWithCoditionMoreThanInOneTable_update (string tableName, string listCoditionMore,string listSymbleMore,string listValueMore,string listCompile)
	{
		string query=getSelectQueryOneAttributeCompareTwoAttribute_update(tableName,listCoditionMore,listSymbleMore,listValueMore,listCompile);
//		DevLog.Log(query);
		DataTable dataTable = db_iOS.ExecuteQuery(query);
		return dataTable;
	}
	
	public DataTable  excuteSelectInOneTableWithListCodiTionAndOr(string strTBName, string listCoditionMore,string listValueMore,string listSymbleMore)
	{
		string query=getSelectQuerylistCoditionAndOrInOneTable(strTBName,listCoditionMore,listValueMore,listSymbleMore);
//		DevLog.Log(query);
		DataTable dataTable = db_iOS.ExecuteQuery(query);
		return dataTable;
	}
	
	public DataTable  excuteSelectInOneTableWithListCodiTionAndOrAndOrderby(string strTBName, string listCoditionMore,string listValueMore,string listSymbleMore,string OrderByList , bool ibASC )
	{
		string query=getSelectQuerylistCoditionAndOrInOneTableAndOrderBy(strTBName,listCoditionMore,listValueMore,listSymbleMore,OrderByList,ibASC);
//		DevLog.Log(query);
		DataTable dataTable = db_iOS.ExecuteQuery(query);
		return dataTable;
	}
	
	public DataTable excuteSelectSpecialListItemInShop()
	{
			string query="SELECT OBJECT_ID,ITEM_NAME as NAME, 1 as KIND, PRICE, IMG_PATH,EFFECT_PUBLIC, EFFECT_PRIVATE 	FROM MT_ITEMS WHERE UNIT_SOLD = 1 UNION SELECT OBJECT_ID,BULLET_NAME as NAME, 2 as KIND, PRICE, IMG_PATH, EFFECT_PUBLIC, EFFECT_PRIVATE FROM MT_BULLET  ORDER BY PRICE ";
//			DevLog.Log(query);
			DataTable dataTable = db_iOS.ExecuteQuery(query);
			return dataTable;
	}
	
	
	
	
////////////////////////////////// ******QUERY STRING ********//////////////////////////////////////////////////////////////////////

////////////////////////////////// SELECT //////////////////////////////////////////////////////////////////////	
	
	
	public string getSelectALLQuery(string strTBName)
	{
		return "SELECT * FROM " +strTBName ;//+" WHERE 1=1 ";
	}		
	public string getSelectQueryWithCondition (string strTBName ,string listTable ,string listCondition){
		
			
		string [] arrayTable =listTable.Split(',');
		string [] arrayCondition =listCondition.Split(',');
		
		string querySelect ="SELECT * FROM ";
		for (int index= 0; index<arrayTable.Length;index++ )
		{
			if(index==arrayTable.Length-1)
			{
				querySelect+=arrayTable[index] +" ";
			}
			else{
				querySelect+=arrayTable[index]+",";
			}
		}
		
		querySelect+=" WHERE " ;
		
		
		Debug.Log("arrayTable.Length "+  arrayTable.Length);
		for (int index= 0; index<arrayTable.Length-1;index++ )
		{		
		
			if(index==arrayTable.Length-2)
			{
				querySelect+= arrayTable[index] +"."+arrayCondition[index] + "=" + arrayTable[index+1]+"."+arrayCondition[index] +";";
			}		
			else{
				querySelect+= arrayTable[index] +"."+arrayCondition[index] + "=" + arrayTable[index+1]+"."+arrayCondition[index] +" and ";
			}
			
		
		}
		return querySelect;
	}
	public string getSelectQueryByListCoditionInOneTable(string strTBName ,string listCondition,string lisVariable)
	{
		string [] arrayCondition =listCondition.Split(',');
		string [] arrayVariable =lisVariable.Split(',');
		string querySelect ="SELECT * FROM " + strTBName +" WHERE ";
		
		
		for (int index= 0; index<arrayCondition.Length;index++ )
		{		
		
			if(index==arrayCondition.Length-1)
			{
				querySelect+= strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +";";
			}		
			else{
				querySelect+= strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +" and ";
			}
			
		
		}
		return querySelect;
		
		
	}	
	public string getSelectQueryByListCoditionDiffNameInManyTable(string listTable,string listCondition)
	{
		string [] arrayTable =listTable.Split(',');
		
		string [] arraCoupleyCondition =listCondition.Split('-');
		
		string querySelect ="SELECT * FROM ";
		for (int index= 0; index<arrayTable.Length;index++ )
		{
			if(index==arrayTable.Length-1)
			{
				querySelect+=arrayTable[index] +" ";
			}
			else{
				querySelect+=arrayTable[index]+",";
			}
		}
		
		querySelect+=" WHERE " ;
		
		
		Debug.Log("arrayTable.Length "+  arrayTable.Length);
		for (int index= 0; index<arrayTable.Length-1;index++ )
		{		
		
			string [] arrayCondition =arraCoupleyCondition[index].Split(',');			
			if(index==arrayTable.Length-2)
			{
				querySelect+= arrayTable[index] +"."+arrayCondition[0] + "=" + arrayTable[index+1]+"."+arrayCondition[1] +";";
			}		
			else{
				querySelect+= arrayTable[index] +"."+arrayCondition[0] + "=" + arrayTable[index+1]+"."+arrayCondition[1] +" and ";
			}
			
		
		}
		return querySelect;
		
	}
	public string getSelectQueryByListCoditionDiffNameInManyTableAndTheSameColumnName(List<object> _arrayListObject,string listTable,string listCondition)
	{
		
		string [] arrayTable =listTable.Split(',');
		string [] arraCoupleyCondition =listCondition.Split('-');
		
		//DevLog.Log("arrayTable lenght :" +arrayTable.Length.ToString());
		//DevLog.Log("arraCoupleyCondition lenght :" +arraCoupleyCondition.Length.ToString());
		
		string querySelect ="SELECT ";
		
		for(int index=0;index<_arrayListObject.Count;index++)
		{
			object _object=_arrayListObject[index];
			foreach(var prop in _object.GetType().GetProperties()) {
				
				querySelect+=arrayTable[index]+"."+prop.Name+" AS " + arrayTable[index] +"___" +prop.Name +" ,";
			}
		}
		
		querySelect = querySelect.Remove(querySelect.Length - 1);
//		DevLog.Log("querySelect :" +querySelect);
		
		
		
		querySelect+=" FROM "+arrayTable[0];
		
		string strInnerJoin ="";
		for (int index= 1; index<arrayTable.Length;index++)
		{
			strInnerJoin+=" INNER JOIN "+arrayTable[index] + "-";
		}
			
		
		
		string strCoditionJoin ="";
		for (int index= 0; index<arrayTable.Length;index++)
		{
			if(index<arrayTable.Length-1)
			{
				string [] arrayCondition =arraCoupleyCondition[index].Split(',');	
				strCoditionJoin+="ON "+arrayTable[index] +"."+arrayCondition[0] + "=" + arrayTable[index+1]+"."+arrayCondition[1] +"-";
			}
		}
		
		//DevLog.Log("strInnerJoin :" +strInnerJoin);
		//DevLog.Log("strCoditionJoin :" +strCoditionJoin);	
		
		
		
		string []A_LINE_INNER =strInnerJoin.Split('-');
		string []A_LINE_ON =strCoditionJoin.Split('-');
		//DevLog.Log("A_LINE_INNER count :" +A_LINE_INNER.Length.ToString());
		//DevLog.Log("A_LINE_ON count :" +A_LINE_ON.Length.ToString());
		for (int index= 0; index<A_LINE_INNER.Length;index++ )
		{
			if(index<A_LINE_INNER.Length-1)
			{
				querySelect += A_LINE_INNER[index] +" " +A_LINE_ON[index]+ " " ;
			}
		}
		
		
		return querySelect;
	}
	// special query - rarely
	public string getselectQueryEditFlagTwoTable( string TableGET ,string TableCompare,string columGet , string coditionColumn,string coditionJoint)
	{
		string querySelect ="SELECT md.*, md2."+columGet +", case when md2."+coditionColumn +">0 then ";		
		querySelect+="md2."+coditionColumn +" else '0' end as NEW_COLUMN FROM ";
		querySelect+=TableGET +" as md LEFT JOIN " + TableCompare + " as md2 ON md" + "." +coditionJoint +" = " +"md2"+"."+coditionJoint;
		
		return querySelect;
	}
	/// LEFT JOINT
	public string getSelectQueryByListCoditionDiffNameInManyTableAndTheSameColumnName_LEFTJOINT(List<object> _arrayListObject,string listTable,string listCondition)
	{
		return "";
	}
	public string getSelectQueryOneAttributeCompareTwoAttribute(string tableName, string listCoditionMore,string listSymbleMore,string listValueMore)
	{
		string querySelect ="SELECT * FROM "+tableName + " WHERE ";
		/*string [] arraylistCodition =listCoditionBy.Split(',');
		string []  arraylistBy= listValueBy.Split(',');
		for(int index=0;index<arraylistCodition.Length;index++)
		{
			
			querySelect+= querySelect[index] + " = "+ arraylistBy[index] + " and ";
			
		}*/
		
		string [] arraylistCoditionMore =listCoditionMore.Split(',');		
		string [] arraylistSymbleMore =listSymbleMore.Split(',');
		string [] arraylistValueMore =listValueMore.Split(',');
		
		
		for(int index=0;index<arraylistCoditionMore.Length;index++)
		{
			if(index==arraylistCoditionMore.Length-1)
			{
				querySelect+=arraylistCoditionMore[index]+ " " + arraylistSymbleMore[index] +" "+arraylistValueMore[index]+" ; ";
			}
			else
			{
				querySelect+=arraylistCoditionMore[index]+ " " + arraylistSymbleMore[index] +" "+arraylistValueMore[index]+" and ";
			}
			
		}
		
		return querySelect;
		
	}
	
	public string getSelectQueryOneAttributeCompareTwoAttribute_update(string tableName, string listCoditionMore,string listSymbleMore,string listValueMore,string listCompile)
	{
		string querySelect ="SELECT * FROM "+tableName + " WHERE ";
		/*string [] arraylistCodition =listCoditionBy.Split(',');
		string []  arraylistBy= listValueBy.Split(',');
		for(int index=0;index<arraylistCodition.Length;index++)
		{
			
			querySelect+= querySelect[index] + " = "+ arraylistBy[index] + " and ";
			
		}*/
		
		string [] arraylistCoditionMore =listCoditionMore.Split(',');		
		string [] arraylistSymbleMore =listSymbleMore.Split(',');
		string [] arraylistValueMore =listValueMore.Split(',');
		string [] arraylistCompile =listCompile.Split(',');
		
		for(int index=0;index<arraylistCoditionMore.Length;index++)
		{
			if(index==arraylistCoditionMore.Length-1)
			{
				querySelect+=arraylistCoditionMore[index]+ " " + arraylistSymbleMore[index] +" "+arraylistValueMore[index]+" ; ";
			}
			else
			{
				querySelect+=arraylistCoditionMore[index]+ " " + arraylistSymbleMore[index] +" "+arraylistValueMore[index]+" " +arraylistCompile[index]+ " " ;
			}
			
		}
		
		return querySelect;
		
	}
	
	
	public string getSelectQuerylistCoditionAndOrInOneTable(string strTBName, string listCoditionMore,string listValueMore,string listSymbleMore)
	{
		
		
		string [] arrayCondition =listCoditionMore.Split(',');
		string [] arrayVariable =listValueMore.Split(',');
		string [] arraySymbleMore=listSymbleMore.Split(',');
			
		string querySelect ="SELECT * FROM " + strTBName +" WHERE ";
		
		
		for (int index= 0; index<arrayCondition.Length;index++ )
		{		
		
			if(index==arrayCondition.Length-1)
			{
				querySelect+= strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +";";
			}		
			else{
				querySelect+= strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +"  " + arraySymbleMore[index] + " ";
			}
			
		
		}
		return querySelect;
	}
	public string getSelectQuerylistCoditionAndOrInOneTableAndOrderBy(string strTBName, string listCoditionMore,string listValueMore,string listSymbleMore,string OrderByList , bool ibASC )
	{
		string [] arrayCondition =listCoditionMore.Split(',');
		string [] arrayVariable =listValueMore.Split(',');
		string [] arraySymbleMore=listSymbleMore.Split(',');
		
		for(int index=0;index<arraySymbleMore.Length;index++)
		{
			//DevLog.Log("AAAA :"+arraySymbleMore[index]);
		}
		
		string querySelect ="SELECT * FROM " + strTBName ;
		
		if(listCoditionMore.Equals("")==false)
		{
			querySelect+=" WHERE ";
			
						
			for (int index= 0; index<arrayCondition.Length;index++ )
			{	
					if(index==arrayCondition.Length-1)
					{
						querySelect+= strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +" ";
					}		
					else{
					
						string _item= arraySymbleMore[index];
						//DevLog.Log("GET ITEM " + index +"  " +_item);
						if(_item.Contains("(")==true||_item.Contains(")")==true)
						{
							if(_item.Contains("(")==true)
							{
								string itemp=_item.Replace("(","");
								querySelect+= "("+strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +"  " + itemp + " ";
							}else if(_item.Contains(")")==true)
							{
								string itemp=_item.Replace(")","");
								querySelect+= strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +"  "+ " ) " + itemp + " ";
							}
							//DevLog.Log("CONTAIN ITEM index "+ index +"  " +_item);
						}
						else
						{
							//DevLog.Log("NO ITEM " + index +"  " +_item);
							querySelect+= strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +"  " + _item + " ";
						}
				}
				
				
				
		
				/*if(index==arrayCondition.Length-1)
				{
					querySelect+= strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +" ";
				}		
				else{
					querySelect+= strTBName +"."+arrayCondition[index] + "=" + arrayVariable[index] +"  " + arraySymbleMore[index] + " ";
				}*/
			
		
				}
			}
		//string OrderByList , bool ibASC
		
		//querySelect
		string [] arrayOrderByList=OrderByList.Split(',');
		if(OrderByList.Equals("")==false)
		{
			querySelect+=" ORDER BY ";
		
		
			for (int index= 0; index<arrayOrderByList.Length;index++ )
			{
				if(index==arrayOrderByList.Length-1)
				{
					querySelect+=arrayOrderByList[index]+ "  ";
				}
				else{
					querySelect+=arrayOrderByList[index]+ " , ";
				}
			}
		
			if(ibASC==false)
			{
				querySelect+= " DESC ";
			}
			else{
				querySelect+= " ASC ";
			}
		}
		
		
		
		
		return querySelect;
	}
	
//////////////////////////////////INSERT ///////////////////////////////////////////////////////////////////////
	public string getQueryInsert(object _object, string table_name = "")
	{
		string nameTable = _object.GetType().BaseType.Name.ToLower();
		if(nameTable.Contains("dbobjectfactory")){
			nameTable  = _object.GetType().Name.ToLower();
		}
		if(!string.IsNullOrEmpty(table_name)){
			nameTable = table_name;
		}
		
		string queryInsert = "INSERT INTO " + nameTable + " (";	
		
		//property
		foreach(var prop in _object.GetType().GetProperties()) {		
			bool flag = false;			
			foreach (Attribute attr in Attribute.GetCustomAttributes(prop)) 
			{
                // Check for the AnimalType attribute. 
                if (attr.GetType() == typeof(UsableProperty))
				{
					flag = true;
					break;					
				}
            }
			
			if(!flag){continue;}
			
			queryInsert += " " + prop.Name + ",";
		}	
		queryInsert = queryInsert.Substring(0, queryInsert.Length - 1) + ") VALUES (";
		   
			///Value
		foreach(var prop in _object.GetType().GetProperties()) {
			
			bool flag = false;			
			foreach (Attribute attr in Attribute.GetCustomAttributes(prop)) 
			{
                // Check for the AnimalType attribute. 
                if (attr.GetType() == typeof(UsableProperty))
				{
					flag = true;
					break;					
				}
            }
			
			if(!flag){continue;}
			
			if(prop.PropertyType == typeof(byte)||prop.PropertyType == typeof(float)||prop.PropertyType == typeof(int)||prop.PropertyType == typeof(decimal))
        	{
         		queryInsert += " " + prop.GetValue(_object,null) + ",";			
        	}
			else
			{			
				queryInsert +=" '"+prop.GetValue(_object,null)+"',";			
			}			
				
		}
		queryInsert = queryInsert.Substring(0, queryInsert.Length - 1) + ");";
		
		 return queryInsert;		
	}
	
	public string getQueryInsert_MaxAutoCounting(string attribute,string tableName)
	{
		string queryMaxAttribute="Select MAX("+attribute+") FROM "+tableName;
		return queryMaxAttribute;
	}
	
	public string getQueryLEFTJointListTableAndListCodition(List<object> _arrayListObject,string listTable,string listCondition,string aTable,string ListWhere,string listValueWhere )
	{
		string [] arrayTable =listTable.Split(',');
		string [] arraCoupleyCondition =listCondition.Split('-');
		
		//DevLog.Log("arrayTable lenght :" +arrayTable.Length.ToString());
		//DevLog.Log("arraCoupleyCondition lenght :" +arraCoupleyCondition.Length.ToString());
		
		string querySelect ="SELECT ";
		
		for(int index=0;index<_arrayListObject.Count;index++)
		{
			object _object=_arrayListObject[index];
			foreach(var prop in _object.GetType().GetProperties()) {
				
				querySelect+=arrayTable[index]+"."+prop.Name+" AS " + arrayTable[index] +"___" +prop.Name +" ,";
			}
		}
		
		querySelect = querySelect.Remove(querySelect.Length - 1);
//		DevLog.Log("querySelect :" +querySelect);
		
		
		
		querySelect+=" FROM "+arrayTable[0];
		
		string strInnerJoin ="";
		for (int index= 1; index<arrayTable.Length;index++)
		{
			strInnerJoin+=" LEFT JOIN "+arrayTable[index] + "-";
		}
			
		
		
		string strCoditionJoin ="";
		for (int index= 0; index<arrayTable.Length;index++)
		{
			if(index<arrayTable.Length-1)
			{
				string [] arrayCondition =arraCoupleyCondition[index].Split('?');	
				
				for(int inJ=0;inJ<arrayCondition.Length;inJ++)
				{
					string[] arrayC=arrayCondition[inJ].Split(',');
					
					
					if(inJ<arrayC.Length-1)
					{
						strCoditionJoin+="ON "+arrayTable[index] +"."+arrayC[0] + "=" + arrayTable[index+1]+"."+arrayC[1] +"-";
					}
					
					else
					{
						strCoditionJoin+="and "+arrayTable[index] +"."+arrayC[0] + "=" + arrayTable[index+1]+"."+arrayC[1] + "-";
					}
					
					
				}
				
				
			}
		}
		
		
		//DevLog.Log("strInnerJoin :" +strInnerJoin);
		//DevLog.Log("strCoditionJoin :" +strCoditionJoin);	
		
		
		
		string []A_LINE_INNER =strInnerJoin.Split('-');
		string []A_LINE_ON =strCoditionJoin.Split('-');
		//DevLog.Log("A_LINE_INNER count :" +A_LINE_INNER.Length.ToString());
		//DevLog.Log("A_LINE_ON count :" +A_LINE_ON.Length.ToString());
		int _J=0;
		int _M=0;
		do
		{
			if(A_LINE_ON[_M].Contains("and")==true)
			{
					//// get more again
					querySelect += " " +A_LINE_ON[_M]+ " " ;
					//J++;
					_M++;
			}
			else
			{	
				querySelect +=A_LINE_INNER[_J] + " " +A_LINE_ON[_M]+ " " ;
				_J++;
				_M++;
			}
			
			
		}while(_J<A_LINE_INNER.Length-1);
		
		
		/*int J=0;
		for (int index= 0; index<A_LINE_ON.Length;index++ )
		{
			if(J<A_LINE_INNER.Length)
			{
				
				if(A_LINE_ON[J].Contains("and")==true)
				{
					//// get more again
					querySelect += " " +A_LINE_ON[J]+ " " ;
					//J++;
				}
				else
				{
					querySelect += A_LINE_INNER[index] +" " +A_LINE_ON[J]+ " " ;
				}
				J++;
			}
		}*/
		
		
		
		querySelect += " WHERE ";
		
		string []listCoditionWhere=ListWhere.Split(',');
		string []listVaWhere=listValueWhere.Split(',');
		
		for (int index= 0; index<listCoditionWhere.Length;index++ )
		{		
		
			if(index==listCoditionWhere.Length-1)
			{
				querySelect+= aTable +"."+listCoditionWhere[index] + "=" + listVaWhere[index] +";";
			}		
			else{
				querySelect+= aTable +"."+listCoditionWhere[index] + "=" + listVaWhere[index] +" and ";
			}
			
		
		}
		return querySelect;
		
		
	}
	
//////////////////////////////////UPDATE ///////////////////////////////////////////////////////////////////////	
	public string getQueryUpdate(string tableName, object _object , string listWhereCodition,string ListValueCodition)
	{
		string [] arrayWhereCodition = null;
		string [] arrayValueCodition = null;
		if(listWhereCodition != null && ListValueCodition != null && !string.Equals(listWhereCodition, "") && !string.Equals(ListValueCodition, "")){
			arrayWhereCodition = listWhereCodition.Split(',');
			arrayValueCodition = ListValueCodition.Split(',');		
		}
		
		string queryUpdate = "UPDATE " + tableName +" SET ";
		
		int i = 0;
		string queryCONDITIONID = "";
		// set value for columns
		foreach(var prop in _object.GetType().GetProperties()) {
			
			bool flag = false;
			bool flagUpdate = true;
			
			foreach (Attribute attr in
                Attribute.GetCustomAttributes(prop)) 
			{
                // Check for the AnimalType attribute. 
                if (attr.GetType() == typeof(UsableProperty))
				{
					flag = true;
				}
				else if(attr.GetType() == typeof(NotQueryProperty) ){
					flagUpdate = false;
				}
            }
			
			if(flag && flagUpdate)
			{
				if(i == 0){
					queryCONDITIONID += prop.Name + "=" + prop.GetValue(_object,null);				
				}
				else{
					if(prop.PropertyType == typeof(byte)||prop.PropertyType == typeof(float)||prop.PropertyType == typeof(int)||prop.PropertyType == typeof(decimal))
		            {
						queryUpdate += prop.Name + "=" + prop.GetValue(_object,null) + ",";
					}else{
						object valueTmp = prop.GetValue(_object, null);
						queryUpdate += prop.Name + "=" + " '" + valueTmp + "' " + ",";
					}
				}
				i++;
			}
		}	
		
		// Add condition where
		queryUpdate = queryUpdate.Substring(0, queryUpdate.Length - 1) + " WHERE ";				
		queryUpdate += queryCONDITIONID;
		if(arrayValueCodition != null && arrayWhereCodition != null){
			
			if(arrayWhereCodition.Length > 0){
				queryUpdate += " AND ";
			}
			for (int index = 0; index < arrayWhereCodition.Length; index++)
			{
				if(index==arrayWhereCodition.Length-1)
				{
					queryUpdate += arrayWhereCodition[index] + " = " + arrayValueCodition[index] + " ; ";
				}
				else 
				{
					queryUpdate += arrayWhereCodition[index] + " = " + arrayValueCodition[index] + " AND ";
				}
				
			}
		}else{
			queryUpdate += " ;";
		}
		return queryUpdate;
		
	}
//////////////////////////////////DELETE ///////////////////////////////////////////////////////////////////////
	public string getQueryDeletebyListCodition(string tableName, object _object, string listWhereCodition, string ListValueCodition)
	{
		string queryDelete = "DELETE FROM " + tableName + " WHERE ";
		
		string [] arrayObject = null;
		string [] arrayVariable = null;
		if(listWhereCodition != null && ListValueCodition != null && !string.Equals(listWhereCodition, "") && !string.Equals(ListValueCodition, "")){
			arrayObject = listWhereCodition.Split(',');
			arrayVariable = ListValueCodition.Split(',');		
		}
		
		foreach(var prop in _object.GetType().GetProperties()) {
			bool flag = false;
			foreach (Attribute attr in
                Attribute.GetCustomAttributes(prop)) 
			{
                // Check for the AnimalType attribute. 
                if (attr.GetType() == typeof(UsableProperty))
				{
					flag = true;
					break;					
				}
            }
			
			if(!flag)
				continue;
			queryDelete += prop.Name + "=" + prop.GetValue(_object,null);
			break;
		}
		
		if(arrayObject != null && arrayVariable != null){
			queryDelete += " and ";
			for(int index = 0; index < arrayObject.Length;index++)
			{			
				if(index==arrayObject.Length-1)
				{
					queryDelete+= arrayObject[index] +" = "+arrayVariable[index]+"; ";
				}		
				else{
					queryDelete+= arrayObject[index] +" = "+arrayVariable[index] +" and ";
				}				
			}
		}
		
		return queryDelete;
	}
	
	
}


 
 

