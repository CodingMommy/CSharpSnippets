/*
In case you haven't encountered this problem before, basically when you try to XML serialise a class that contains a Uri
you will get an error saying Uri can not be XML serialised, so you have to add this little workaround to tell the
XML serialiser to ignore the Uri "Uri1" and then when it tries to serialise the "Uri1String" the previously ignored
Uri "Uri1" will be converted to a string and vice versa.

This can be helpful when trying to serialise a collection to export and import it as an XML file and where you want
the Uri to be accessible as such (perhaps to be used as a Binding Source for an image in a GridView in XAML) and it
doesn't rely on the conversion every time "Uri1" needs to be accessed, only when it is serialised or de-serialised.

This example is of a class called 'User'
*/

public class User
    {
        public string Username { get; set; }
        public int UserID { get; set; }

        [XmlIgnore]
        public Uri Uri1 { get; set; }

        /*
        Tell XML serialiser to call this "Uri" and make it not
        browsable in the editor (Isn't required when constructing)
        */

        [XmlAttribute("Uri")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Uri1String
        {
            get { return Uri1 == null ? null : Uri1.OriginalString; }
            set { Uri1 = value == null ? null : new Uri(value); }
        }

        /* 
        The above getter and setter will convert the Uri "Uri1"
        to and from a string when an attempt is made to XML Serialise
        and de-serialise it
        */
    }