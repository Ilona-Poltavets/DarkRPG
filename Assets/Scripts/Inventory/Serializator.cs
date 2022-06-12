using System.Xml.Serialization;
using System;
using System.IO;
using System.Collections.Generic;

public class Serializator
{
	static public void SaveXml(Characteristics state, string datapath)
	{

		Type[] extraTypes = { typeof(Characteristics) };
		XmlSerializer serializer = new XmlSerializer(typeof(Characteristics), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Create);
		serializer.Serialize(fs, state);
		fs.Close();

	}

	static public Characteristics DeXml(string datapath)
	{

		Type[] extraTypes = { typeof(Characteristics) };
		XmlSerializer serializer = new XmlSerializer(typeof(Characteristics), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Open);
		Characteristics state = (Characteristics)serializer.Deserialize(fs);
		fs.Close();

		return state;
	}

	static public void SaveInventory(InventoryManager state, string datapath)//(List<Item> state, string datapath)
	{
		Type[] extraTypes = { typeof(InventoryManager) };
		XmlSerializer serializer = new XmlSerializer(typeof(InventoryManager), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Create);
		serializer.Serialize(fs, state);
		fs.Close();
	}
	static public InventoryManager GetInventory(string datapath)//(List<Item> state, string datapath)
	{
		Type[] extraTypes = { typeof(InventoryManager) };
		XmlSerializer serializer = new XmlSerializer(typeof(InventoryManager), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Open);
		InventoryManager state = (InventoryManager)serializer.Deserialize(fs);
		fs.Close();

		return state;
	}
}
