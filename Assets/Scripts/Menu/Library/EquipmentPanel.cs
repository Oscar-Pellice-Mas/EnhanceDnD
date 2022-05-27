using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text category;
    [SerializeField] private TMP_Text weapon;
    [SerializeField] private TMP_Text properties;
    [SerializeField] private TMP_Text cost;
    [SerializeField] private TMP_Text armor;
    [SerializeField] private TMP_Text vehicle;
    [SerializeField] private TMP_Text gear;
    [SerializeField] private TMP_Text tools;
    [SerializeField] private TMP_Text others;

    public void SetEquipment(DB.Equipment equipment)
    {
        string aux = "";

        // Name
        title.text = "<size=200%>" + equipment.name;

        // Category
        aux = "\n<b>Category</b>\n";
        aux += equipment.equipment_category;
        category.text = aux;

        // Weapon
        aux = "\n<b>Weapon</b>\n";
        if (equipment.equipment_category == "weapon")
        {
            aux += equipment.weapon_category + "\n";
            aux += equipment.weapon_range + "(" + equipment.category_range + ")\n";
            weapon.text = aux;

            aux = "Properties: ";
            foreach (string pro in equipment.properties)
            {
                aux += pro + ", ";
            }

            aux += "\n\nDamage: " + equipment.damage.damage_dice + "[" + equipment.damage.damage_type + "] - (" + equipment.two_handed_damage.damage_dice + ")";

            aux += "\n\nRange: " + equipment.range.normal_range + "/" + equipment.range.long_range 
                + "(" + equipment.throw_range.normal_range + "/" + equipment.throw_range.long_range + ")";
            properties.text = aux;
        }

        // Cost
        aux = "\n<b>Cost</b>\n";
        aux += "Cost: " + equipment.cost.quantity + equipment.cost.unit;
        aux += "\nWeight:" + equipment.weight;
        cost.text = aux;

        // Armor
        aux = "\n<b>Armor</b>\n";
        if (equipment.equipment_category == "armor")
        {
            aux += equipment.armor_category + "AC " + equipment.armor_class.max_bonus; 
        }
        armor.text = aux;

        // Vehicle
        aux = "\n<b>Vehicle</b>\n";
        if (equipment.equipment_category == "vehicle")
        {
            aux += equipment.vehicle_category + "Speed: " + equipment.speed;
        }
        vehicle.text = aux;

        // Gear
        aux = "\n<b>Gear</b>\n";
        if (equipment.equipment_category == "adventuring-gear")
        {
            aux += equipment.gear_category;
        }
        gear.text = aux;

        // Tool
        aux = "\n<b>Tool</b>\n";
        if (equipment.equipment_category == "tools")
        {
            aux += equipment.tool_category;
        }
        tools.text = aux;

        // Others
        others.text = "";
        if (equipment.desc.Count != 0)
        {
            aux = "\n<b>Description</b>\n";
            foreach (string str in equipment.desc)
            {
                aux += str + "\n";
            }
            others.text += aux + "\n";
        }

        if (equipment.contents.Count != 0)
        {
            aux = "\n<b>Description</b>\n";
            foreach (DB.Content cont in equipment.contents)
            {
                aux += cont.item + "x" + cont.quantity + "\n";
            }
            others.text += aux + "\n";
        }

        if (equipment.capacity != "")
        {
            others.text += equipment.capacity + "\n";
        }

        this.GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}
