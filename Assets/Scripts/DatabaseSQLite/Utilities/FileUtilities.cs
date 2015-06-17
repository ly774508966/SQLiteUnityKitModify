using UnityEngine;
using System.Collections;
using System.IO;

public class FileUtilities {

// Saving file byte byte
	public static void writeFileToDocument(string path, string strName,byte[] arrayByte)//".png"
	{		

		string url_path = path + "/" + strName;
		File.WriteAllBytes(url_path, arrayByte);
		//Debug.Log("Done writeFileToDocument:" + url_path);
	}
	
//	public void SaveFileByByteArray(byte[] arrayByte)
//	{
//		DevLog.Log("arraybyte " +arrayByte.Length);
//		SaveFile("",ConstantsHelpGacha.dbName,arrayByte);
//	}
	
	
	public static void SaveFile(string urlPath,string Name,byte[] arrayByte){
	
//#if UNITY_EDITOR
//		//string tempUrl = Application.dataPath + "/StreamingAssets/" + urlPath;
//#else
		string tempUrl = Application.persistentDataPath + "/" + urlPath;
//#endif
		
		
		if(!Directory.Exists(tempUrl)){	
			Directory.CreateDirectory(tempUrl);				
		}	
		
		writeFileToDocument(tempUrl,Name,arrayByte);		
		
    }
	
	public static string ReadFile(string urlPath,string Name) 
	{
		string tempUrl  = Application.persistentDataPath + "/" + urlPath;
		if(!Directory.Exists(tempUrl)){	
			return "";				
		}
		string _urlPath =tempUrl+"/"+ Name;
		string result=File.ReadAllText(_urlPath);
		
		return result;
	}
	
	public static bool checkFileExistInDocument(string path, string strName)
	{
		string urlPath  = Application.persistentDataPath + "/" + path + "/"+strName;
		if(File.Exists(urlPath)==true)
		{
			return true;
		}
		
		return false;
	}	// Use this for initialization

	public static IEnumerator LoadFile(string path, System.Action<string> onreturn)
	{			
		
		if( !path.Substring(0,7).ToUpper().Equals("FILE://") )
			path = "file://" + path;

		//Debug.Log ("Load file at path: " + path);

		WWW www = new WWW (path);
		
		yield return www;
			
		string  text = null;

		if (www.isDone) 
		{
			if (string.IsNullOrEmpty (www.error)) 
			{
				text = www.text;
			}
		}
		
		if(onreturn != null)
			onreturn(text);
	}

	public static IEnumerator LoadFile(string path, System.Action<Texture2D> onreturn)
	{			
		//Debug.Log ("1. Load file at path: " + path);
	
		if( !path.Substring(0,7).ToUpper().Equals("FILE://") )
			path = "file://" + path;

		WWW www = new WWW (path);
		
		yield return www;
		
		Texture2D  data = null;

		//Debug.Log ("2. Load file done: " + path);

		if (www.isDone) 
		{
			if (string.IsNullOrEmpty (www.error)) 
			{
				//Debug.Log ("3. Is done");

				data = www.texture;
			}
		}
		
		if(onreturn != null)
			onreturn(data);
	}

	public static IEnumerator LoadFile(string path, System.Action<byte[]> onreturn)
	{			
		
		if( !path.Substring(0,7).ToUpper().Equals("FILE://") )
			path = "file://" + path;

		yield return new WaitForSeconds(0.5f);

		//Debug.Log ("Load file at path: " + path);

		WWW www = new WWW (path);
		
		yield return www;
		
		byte[] data = null;
		
		if (www.isDone) 
		{
			if (string.IsNullOrEmpty (www.error)) 
			{
                data = new byte[www.bytesDownloaded];

                www.bytes.CopyTo(data, 0);
				//Debug.Log ("File done loading: " + path);
			}
		}

        www.Dispose();
		
		if(onreturn != null)
			onreturn(data);
	}

	public static IEnumerator LoadFile(string path, System.Action<AudioClip> onreturn)
	{			
		
		if( !path.Substring(0,7).ToUpper().Equals("FILE://") )
			path = "file://" + path;

		yield return new WaitForSeconds(0.5f);
		
		//Debug.Log ("Load file at path: " + path);
		
		WWW www = new WWW (path);
		
		yield return www;
		
		AudioClip data = null;
		
		if (www.isDone) 
		{
			if (string.IsNullOrEmpty (www.error)) 
			{
				//data = www.audioClip;
				data = www.GetAudioClip(false, false);
				//Debug.Log ("File done loading: " + path);
			}
		}
		
		www.Dispose();
		
		if(onreturn != null)
			onreturn(data);
	}
}
