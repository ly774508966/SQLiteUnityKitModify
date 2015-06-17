using UnityEngine;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

public abstract class SerializableObject<T> : System.Object
	where T : SerializableObject<T>, new()
{

	// Use this for initialization
	public static T Deserialize(string file_name, System.Type[] extraTypes)
	{

		// To read the file, create a FileStream.
		
		TextAsset textAssetXML = Resources.Load<TextAsset>(file_name);

		return (T)Deserialize(textAssetXML.bytes, extraTypes);
	}

	public static T Deserialize(byte[] data, System.Type[] extraTypes)
	{
        T myObject;

		using(MemoryStream assetStream = new MemoryStream(data))
        {
    		// Construct an instance of the XmlSerializer with the type
    		// of object that is being deserialized.
    		XmlSerializer mySerializer = 
    			new XmlSerializer(typeof(T), extraTypes);
    		
    		// Call the Deserialize method and cast to the object type.
    		myObject = (T)mySerializer.Deserialize(assetStream);

        }
		return myObject;
	}
	
	public void Serialize(string file_name, System.Type[] extraTypes)
	{
		XmlSerializer mySerializer = new XmlSerializer(this.GetType(), extraTypes);
		// To write to a file, create a StreamWriter object.
		StreamWriter myWriter = new StreamWriter(file_name);
		mySerializer.Serialize(myWriter, this);
		myWriter.Close();
	}
}

