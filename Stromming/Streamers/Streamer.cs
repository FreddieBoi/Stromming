namespace Stromming.Streamers {

    interface IStreamer {

        string Name { get; }

        long Count { get; }

        void Search(string term);

    }

}
