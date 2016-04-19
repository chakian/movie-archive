using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MArchiveLibrary.ObjectMapping
{
    public class ObjectMapper
    {
        public static void CopyObject<T>(T from, T to, params string[] ExcludedFieldNames)
        {
            Type tto;
            if (from.GetType() == to.GetType())
                tto = from.GetType();
            else
                tto = typeof(T);

            PropertyInfo[] pFrom = tto.GetProperties();
            // get metadata type attributes.
            MetadataTypeAttribute[] metaAttr = (MetadataTypeAttribute[])tto.GetCustomAttributes(typeof(MetadataTypeAttribute), true);

            Type metadataT;
            PropertyInfo metadataPI;
            bool doCopy;
            foreach (PropertyInfo p in pFrom)
            {
                PropertyInfo tmp = tto.GetProperty(p.Name);
                doCopy = true;

                if (ExcludedFieldNames.Contains(p.Name) || (tmp.PropertyType.Assembly.GetName().Name != "mscorlib"))
                {
                    doCopy = false;
                }

                if ((p.PropertyType.FullName == "System.Data.Linq.Binary" && p.Name == "VerCol"))// yukarıdaki if ile de birleşebilirdi ama böyle daha okunaklı anlaşılır geldi
                    doCopy = true;

                if (doCopy)
                {
                    bool noCopyDefined = OmitMapping.IsDefined(tmp, typeof(OmitMapping));
                    if (noCopyDefined)
                        doCopy = false;
                }
                if (doCopy)
                {
                    // check metadata type attributes as well.
                    if (metaAttr.Length > 0)
                    {
                        foreach (MetadataTypeAttribute attr in metaAttr)
                        {
                            metadataT = attr.MetadataClassType;
                            metadataPI = metadataT.GetProperty(p.Name);
                            if (metadataPI != null)
                            {
                                bool noCopyDefined = OmitMapping.IsDefined(metadataPI, typeof(OmitMapping));
                                if (noCopyDefined)
                                    doCopy = false;
                            }
                        }
                    }
                }

                if (doCopy)
                {
                    if (tmp == null || (tmp.CanWrite == false))
                        continue;
                    tmp.SetValue(to, p.GetValue(from, null), null);
                }
            }
        }

        public static void MapObjects<T1, T2>(T1 from, T2 to, params string[] ExcludedFieldNames)
        {
            Type tfrom = typeof(T1);
            Type tto = typeof(T2);
            PropertyInfo[] pFrom = tfrom.GetProperties();
            PropertyInfo[] pTo = tto.GetProperties();
            // get metadata type attributes.
            MetadataTypeAttribute[] metaAttrFrom = (MetadataTypeAttribute[])tfrom.GetCustomAttributes(typeof(MetadataTypeAttribute), true);
            MetadataTypeAttribute[] metaAttrTo = (MetadataTypeAttribute[])tto.GetCustomAttributes(typeof(MetadataTypeAttribute), true);

            Type metadataT1;
            Type metadataT2;
            PropertyInfo metadataPIT1;
            PropertyInfo metadataPIT2;
            bool doCopy;
            foreach (PropertyInfo p in pFrom)
            {
                //TODO aslında bu yetmez attribute da ki isim tutuyormu
                // yada attribute larındaki isimlerle bu yada attr ismi tutuyormu
                // türü bişi olması lazım
                // şu hali sadece aynı isimli alanları mapler
                if (pTo.SingleOrDefault(prop => prop.Name.Equals(p.Name)) != null)
                {
                    PropertyInfo tmp = tto.GetProperty(p.Name);
                    doCopy = true;

                    if (ExcludedFieldNames != null)
                    {
                        if (ExcludedFieldNames.Contains(p.Name) || (tmp.PropertyType.Assembly.GetName().Name != "mscorlib"))
                        {
                            doCopy = false;
                        }
                    }

                    if ((p.PropertyType.FullName == "System.Data.Linq.Binary" && p.Name == "VerCol"))// yukarıdaki if ile de birleşebilirdi ama böyle daha okunaklı anlaşılır geldi
                        doCopy = true;

                    if (doCopy)
                    {
                        bool fromOmitsMapping = OmitMapping.IsDefined(p, typeof(OmitMapping));
                        bool toOmitsMapping = OmitMapping.IsDefined(tmp, typeof(OmitMapping));
                        if (fromOmitsMapping || toOmitsMapping)
                            doCopy = false;
                    }
                    if (doCopy)
                    {
                        // check metadata type attributes of from type as well.
                        if (metaAttrFrom.Length > 0)
                        {
                            foreach (MetadataTypeAttribute attr in metaAttrFrom)
                            {
                                metadataT1 = attr.MetadataClassType;
                                metadataPIT1 = metadataT1.GetProperty(p.Name);

                                if (metadataPIT1 != null)
                                {
                                    bool noCopyDefined = OmitMapping.IsDefined(metadataPIT1, typeof(OmitMapping));
                                    if (noCopyDefined)
                                        doCopy = false;
                                }
                            }
                        }

                        // check metadata type attributes of to type as well.
                        if (metaAttrTo.Length > 0)
                        {
                            foreach (MetadataTypeAttribute attr in metaAttrTo)
                            {
                                metadataT2 = attr.MetadataClassType;
                                metadataPIT2 = metadataT2.GetProperty(p.Name);

                                if (metadataPIT2 != null)
                                {
                                    bool noCopyDefined = OmitMapping.IsDefined(metadataPIT2, typeof(OmitMapping));
                                    if (noCopyDefined)
                                        doCopy = false;
                                }
                            }
                        }
                    }
                    if (doCopy)
                    {
                        if (tmp == null || (tmp.CanWrite == false))
                            continue;
                        tmp.SetValue(to, p.GetValue(from, null), null);
                    }
                }
            }
        }
    }
}
