using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_import_Features : MonoBehaviour
{
    // Temporal
    [System.Serializable]
    public class JS_Class
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Prerequisite
    {
        public string type;
        public string spell;
        public string feature;
        public int? level;
    }

    [System.Serializable]
    public class JS_Subclass
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_From
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_ExpertiseOptions
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_SubfeatureOptions
    {
        public int choose;
        public string type;
        public List<JS_From> from;
    }

    [System.Serializable]
    public class JS_FeatureSpecific
    {
        public JS_ExpertiseOptions expertise_options;
        public JS_SubfeatureOptions subfeature_options;
    }

    [System.Serializable]
    public class JS_Parent
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class JS_Root
    {
        public string index;
        public JS_Class @class;
        public string name;
        public int level;
        public List<JS_Prerequisite> prerequisites;
        public List<string> desc;
        public string url;
        public JS_Subclass subclass;
        public string reference;
        public JS_FeatureSpecific feature_specific;
        public JS_Parent parent;
    }

    // Start is called before the first frame update
    void Start()
    {
        string inputPath = Application.dataPath + "/Files/Temporal/5e-SRD-Features.json";
        string jsonString = System.IO.File.ReadAllText(inputPath);
        JS_Root[] jsonObject = JsonHelper.FromJson<JS_Root>(jsonString);

        DB.Feature[] features = new DB.Feature[jsonObject.Length];
        for (int i = 0; i < jsonObject.Length; i++)
        {
            DB.Feature aux = new DB.Feature();

            //public string index;
            aux.index = jsonObject[i].index;
            //public string @class;
            aux.feature_class = jsonObject[i].@class.index;
            //public string name;
            aux.name = jsonObject[i].name;
            //public int level;
            aux.level = jsonObject[i].level;
            //public List<Prerequisite> prerequisites;
            aux.prerequisites = new List<DB.FeaturePrerequisite>();
            foreach (JS_Prerequisite prerequisite in jsonObject[i].prerequisites)
            {
                DB.FeaturePrerequisite auxiliar = new DB.FeaturePrerequisite();
                auxiliar.feature = prerequisite.feature;
                auxiliar.level = prerequisite.level;
                auxiliar.spell = prerequisite.spell;
                auxiliar.type = prerequisite.type;
                aux.prerequisites.Add(auxiliar);
            }
            //public List<string> desc;
            aux.desc = new List<string>();
            foreach (string description in jsonObject[i].desc)
            {
                aux.desc.Add(description);
            }
            //public string subclass;
            aux.subclass = jsonObject[i].subclass.index;
            //public string reference;
            aux.reference = jsonObject[i].reference;
            //public FeatureSpecific feature_specific;
            if (jsonObject[i].feature_specific != null){ 
                aux.feature_specific = new DB.FeatureSpecific();
                if(jsonObject[i].feature_specific.expertise_options != null)
                {
                    aux.feature_specific.expertise_options = new DB.ExpertiseOptions();
                    aux.feature_specific.expertise_options.choose = jsonObject[i].feature_specific.expertise_options.choose;
                    aux.feature_specific.expertise_options.type = jsonObject[i].feature_specific.expertise_options.type;
                    if (jsonObject[i].feature_specific.expertise_options.from != null)
                    {
                        aux.feature_specific.expertise_options.fromExpertises = new List<string>();
                        foreach (JS_From from in jsonObject[i].feature_specific.expertise_options.from)
                        {
                            aux.feature_specific.expertise_options.fromExpertises.Add(from.index);
                        }
                    }
                }

                if (jsonObject[i].feature_specific.subfeature_options != null)
                {
                    aux.feature_specific.subfeature_options = new DB.SubfeatureOptions();
                    aux.feature_specific.subfeature_options.choose = jsonObject[i].feature_specific.subfeature_options.choose;
                    aux.feature_specific.subfeature_options.type = jsonObject[i].feature_specific.subfeature_options.type;
                    if (jsonObject[i].feature_specific.subfeature_options.from != null)
                    {
                        aux.feature_specific.subfeature_options.fromFeatures = new List<string>();
                        foreach (JS_From from in jsonObject[i].feature_specific.subfeature_options.from)
                        {
                            aux.feature_specific.subfeature_options.fromFeatures.Add(from.index);
                        }
                    }
                }
            }
            
            //public string parent;
            aux.parent = jsonObject[i].parent.index;

            features[i] = aux;
        }

        string jsonStringOut = JsonHelper.ToJson(features);
        string outputPath = Application.dataPath + "/Files/Definitive/Features.json";
        System.IO.File.WriteAllText(outputPath, jsonStringOut);

    }
}
