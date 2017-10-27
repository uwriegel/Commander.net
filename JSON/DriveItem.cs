using System.Runtime.Serialization;

[DataContract]
struct DriveItem
{
    [DataMember]
    public string name;
    [DataMember]
    public string description;
    [DataMember]
    public long size; 
    [DataMember]
    public bool isNetworkDrive;
}