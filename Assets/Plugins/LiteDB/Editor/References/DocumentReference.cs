using System.ComponentModel;

namespace LiteDB.Editor
{
    public class DocumentReference : INotifyPropertyChanged
    {
        private BsonDocument document;
        public BsonDocument LiteDocument
        {
            get
            {
                return document;
            }

            set
            {
                document = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LiteDocument"));
            }
        }

        private CollectionReference collection;
        public CollectionReference Collection
        {
            get
            {
                return collection;
            }

            set
            {
                collection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Collection"));
            }
        }

        public DocumentReference()
        {
        }

        public DocumentReference(BsonDocument document, CollectionReference collection)
        {
            LiteDocument = document;
            Collection = collection;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
