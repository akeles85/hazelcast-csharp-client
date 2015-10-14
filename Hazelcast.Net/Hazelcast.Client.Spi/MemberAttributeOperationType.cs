namespace Hazelcast.Client.Spi
{
	/// <summary>Used to identify the type of member attribute change, either PUT or REMOVED</summary>
	public enum MemberAttributeOperationType
	{
		PUT=1,
		REMOVE=2
	}
}