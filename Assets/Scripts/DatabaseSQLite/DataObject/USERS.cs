using UnityEngine;
using System.Collections;

public class USERS : DBObjectFactory<USERS>
{
	public const string tableName = "user";
	
	[UsableProperty]
	public int id { get; set; }

	[UsableProperty]
	public string username { get; set; }

	[UsableProperty]
	public int age { get; set; }

	[UsableProperty]
	public string company { get; set; }



}
