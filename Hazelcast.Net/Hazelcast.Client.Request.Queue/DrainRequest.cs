using Hazelcast.IO.Serialization;
using Hazelcast.Serialization.Hook;

namespace Hazelcast.Client.Request.Queue
{
    public class DrainRequest : QueueRequest
    {
        internal int maxSize;

        public DrainRequest()
        {
        }

        public DrainRequest(string name, int maxSize) : base(name)
        {
            this.maxSize = maxSize;
        }

        public override int GetClassId()
        {
            return QueuePortableHook.Drain;
        }

        /// <exception cref="System.IO.IOException"></exception>
        public override void WritePortable(IPortableWriter writer)
        {
            base.WritePortable(writer);
            writer.WriteInt("m", maxSize);
        }

        /// <exception cref="System.IO.IOException"></exception>
        public override void ReadPortable(IPortableReader reader)
        {
            base.ReadPortable(reader);
            maxSize = reader.ReadInt("m");
        }
    }
}