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

	static public void SaveInventory(List<Item> state, string datapath)//(InventoryManager state, string datapath)
	{
		Type[] extraTypes = { typeof(List<Item>) };
		XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Create);
		serializer.Serialize(fs, state);
		fs.Close();
	}
	static public List<Item> GetInventory(string datapath)//(List<Item> state, string datapath)
	{
		Type[] extraTypes = { typeof(List<Item>) };
		XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Open);
		List<Item> state = (List<Item>)serializer.Deserialize(fs);
		fs.Close();

		return state;
	}
	static public void SaveEquipment(SerializableDictionary<string, Item> state, string datapath)//(InventoryManager state, string datapath)
	{
		Type[] extraTypes = { typeof(SerializableDictionary<string,Item>) };
		XmlSerializer serializer = new XmlSerializer(typeof(SerializableDictionary<string, Item>), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Create);
		serializer.Serialize(fs, state);
		fs.Close();
	}
	static public SerializableDictionary<string, Item> GetEquipment(string datapath)//(List<Item> state, string datapath)
	{
		Type[] extraTypes = { typeof(SerializableDictionary<string, Item>) };
		XmlSerializer serializer = new XmlSerializer(typeof(SerializableDictionary<string, Item>), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Open);
		SerializableDictionary<string, Item> state = (SerializableDictionary<string, Item>)serializer.Deserialize(fs);
		fs.Close();

		return state;
	}
	static public void SaveSettings(Settings state, string datapath)//(InventoryManager state, string datapath)
	{
		Type[] extraTypes = { typeof(Settings) };
		XmlSerializer serializer = new XmlSerializer(typeof(Settings), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Create);
		serializer.Serialize(fs, state);
		fs.Close();
	}
	static public Settings GetSettings(string datapath)//(List<Item> state, string datapath)
	{
		Type[] extraTypes = { typeof(Settings) };
		XmlSerializer serializer = new XmlSerializer(typeof(Settings), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Open);
		Settings state = (Settings)serializer.Deserialize(fs);
		fs.Close();

		return state;
	}
}
