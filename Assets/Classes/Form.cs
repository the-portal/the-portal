using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AssemblyCSharp
{

	public class Form
	{
		public IDictionary<string,string> fields;
		public IDictionary<string,GameObject> actualsfields;
		public GameObject[] inputFields;
		public Form()
		{
			GetFieldValues();
		}

		public void SetForm(string[] fieldsData){
			int x = 1;//no id field in form
			foreach(GameObject inputField in inputFields)
			{
				if(inputField.GetComponent<Dropdown>())
				{
					inputField.GetComponent<Dropdown>().captionText.text = fieldsData[x++];
				}

				if(inputField.GetComponent<InputField>())
				{
					inputField.GetComponent<InputField>().text = fieldsData[x++];
				}

				if(inputField.GetComponent<Button>())
				{
					inputField.GetComponentInChildren<Text>().text = fieldsData[x++];
				}
			}
		}


		public void SetForm(IDictionary<string,string> fieldsData)
		{
//			int x = 0;
			foreach(KeyValuePair<string,string> kvp in fieldsData)
			{
				if(actualsfields.ContainsKey(kvp.Key))
				{

					if(actualsfields[kvp.Key].GetComponent<Dropdown>())
					{
						actualsfields[kvp.Key].GetComponent<Dropdown>().captionText.text = kvp.Value;
					}

					if(actualsfields[kvp.Key].GetComponent<InputField>())
					{
						actualsfields[kvp.Key].GetComponent<InputField>().text = kvp.Value;
					}

					if(actualsfields[kvp.Key].GetComponent<Button>())
					{
						actualsfields[kvp.Key].GetComponentInChildren<Text>().text = kvp.Value;
					}	
				}
				//x++;

//				actualsfields[kvp.Key].text = kvp.Value;
//				if(actualsfields.ContainsKey(kvp.Key))
//				{ 
//					actualsfields[kvp.Key].text = kvp.Value;
//					Debug.Log(actualsfields[kvp.Key].name);
//				}
			//	Debug.Log(kvp.Key);
			}
		}

		public void ResetForm(){
			foreach(GameObject inputField in inputFields)
			{
				//Debug.Log(inputField.name);
				if(inputField.GetComponent<Dropdown>())
				{
					inputField.GetComponent<Dropdown>().value = 0;
				}

				if(inputField.GetComponent<InputField>())
				{
					inputField.GetComponent<InputField>().text = "";
					inputField.GetComponent<InputField>().placeholder.enabled = true;
				}

				if(inputField.GetComponent<Button>())
				{
					char[] data = inputField.name.ToCharArray();
					inputField.GetComponentInChildren<Text>().text = data[0].ToString().ToUpper() +  inputField.name.Remove(0,1);
				}
			}
		}


		public void GetFieldValues()
		{
			inputFields = GameObject.FindGameObjectsWithTag("inputfield");
			fields = new Dictionary<string,string>();
			actualsfields = new Dictionary<string,GameObject>();
			foreach(GameObject inputField in inputFields)
			{
				actualsfields[inputField.name] = inputField;

				if(inputField.GetComponent<Dropdown>())
				{
					fields[inputField.name] = inputField.GetComponent<Dropdown>().captionText.text;
				}

				if(inputField.GetComponent<InputField>())
				{
					fields[inputField.name] = inputField.GetComponent<InputField>().text;
				}

				if(inputField.GetComponent<Button>())
				{
					fields[inputField.name] = inputField.GetComponentInChildren<Text>().text;
				}
			}
		}
			
		/*public string EmailValid()
		{
			bool isEmail = Regex.IsMatch(fields["email"], @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
			{	
				return IsInvalidCheck(isEmail,"email");
			}
		}

		public  string IsIntAndPositive(InputField field)
		{
			int number = 0;
			bool valid = IsInt(field.text,ref number);

			if (valid)
			{
				if(IsNumberNegative(number)) valid = false;
			}

			return IsInvalidCheck(valid, field);

		}

		public  string IsIFloatAndPositive(InputField field)
		{
			float number = 0;
			bool valid = IsFloat(field.text, ref number);

			if (valid)
			{
				if (IsNumberNegative(number)) valid = false;
			}

			return IsInvalidCheck(valid, field);

		}

		public  bool IsInt(string input, ref int number)
		{
			bool isInt = int.TryParse(input, out number);
			return isInt;

		}

		public  bool IsFloat(string input, ref float number)
		{
			bool isFloat = float.TryParse(input, out number);
			return isFloat;

		}

		public  bool IsNumberNegative(float number)
		{
			return (number < 0);
		}

		public  string IsPhoneNumber(InputField field)
		{
			IsIntAndPositive(field);
			bool valid = (field.text.Length==10)? true : false;

			return IsInvalidCheck(valid, field);
		}*/

		public  string Empty(KeyValuePair<string,string> kvp)
		{
			//Debug.Log( kvp.Key + " " + kvp.Value);
			return (kvp.Value.Length==0)?"Required " + kvp.Key.Replace('_',' ') + "\n": null;
		}
	
		public string PasswordMatch()
		{
			return (fields["password"] != fields["confirm_password"])?"Passwords don't match\n": "";
		}

		public  string CheckEmptyFields()
		{
			string errorMessages = null;

			foreach(KeyValuePair<string,string>kvp in fields)
			{
				errorMessages += Empty(kvp);	
			}
			return errorMessages;
		}

		/*public  string IsInvalidCheck(bool check, string fieldName)
		{
			return (!check) ? "Invalid " + fieldName + "\n" : null;
		}


		public  string FieldsValidtionsExplorer(InputField email,InputField num,InputField password,InputField confirmPassword )
		{
			return(EmailValid(email) + IsIntAndPositive(num) + PasswordMatch(password,confirmPassword)); 
		}

		public  string FieldsValidtionsPromoter(InputField email, InputField telNo, InputField password, InputField confirmPassword)
		{
			return (EmailValid(email) + IsPhoneNumber(telNo) + PasswordMatch(password, confirmPassword));
		}

		public  string FieldsValidtionsEvents(InputField price)
		{
			return (IsIFloatAndPositive(price));
		}*/
	}
}