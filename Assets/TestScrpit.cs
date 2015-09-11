using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Collections;
using u16 = System.UInt16;
using System;

public class TestScrpit : MonoBehaviour
{

	public Text myText;
	public Text versionText;

	protected enum State
	{
		HOME,
		TEST_METEO,
		TEST_COLOR,
		ERROR}
	;
	protected State state;
	protected string errmsg;


	protected string meteoSerial = null;
	protected string colorSerial = null;
	YTemperature temperatureSensor = null;
	YHumidity humiditySensor = null;
	YPressure pressureSensor = null;
	YColorLed led1 = null, led2 = null;

	// Use this for initialization
	void Start ()
	{
		string version;
		if (IntPtr.Size == 4) {
			version = "Unity: 32 bits";
		} else {
			version = "Unity: 64 bits";
		}
		version += " / Yoctopuce:" + YAPI.GetAPIVersion ();
		versionText.text = version;
		string msg = "";
		if (YAPI.RegisterHub ("usb", ref msg) != YAPI.SUCCESS) {
			state = State.ERROR;
			errmsg = msg;
			return;
		}
		YModule module = YModule.FirstModule ();
		while (module!=null) {
			string product = module.get_productName ();
			string serial = module.get_serialNumber ();
			if (product == "Yocto-Meteo") {
				meteoSerial = serial;
				temperatureSensor = YTemperature.FindTemperature (serial + ".temperature");
				humiditySensor = YHumidity.FindHumidity (serial + ".humidity");
				pressureSensor = YPressure.FindPressure (serial + ".pressure");
			} else if (product == "Yocto-Color") {
				colorSerial = serial;
				led1 = YColorLed.FindColorLed (serial + ".colorLed1");
				led2 = YColorLed.FindColorLed (serial + ".colorLed2");
			}				 
			module = module.nextModule ();
		}
		if (meteoSerial == null) {
			errmsg = "No Yocto-Meteo detected";
			state = State.ERROR;
		} else if (colorSerial == null) {
			errmsg = "No Yocto-color detected";
			state = State.ERROR;
		} else {
			state = State.HOME;
		}


	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (state) {
		case State.HOME:
			update_home ();
			break;
		case State.TEST_METEO:
			update_meteo ();
			break;
		case State.TEST_COLOR:
			update_color ();
			break;
		case State.ERROR:
			update_error ();
			break;
		}
	}

	protected void update_home ()
	{
		myText.text = "This is a demo that illustrate the usage of the Yoctopuce C# library in a Unity 5 project. This \"game\"will work on Windows, Linux, and OS X."
			+ "\n\nPress Space to continue or Esc to Exit.";
		if (colorSerial != null) {
			led1.set_rgbColor (0);
			led2.set_rgbColor (0);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {

			Application.Quit ();
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			state = State.TEST_METEO;
		}
	}

	protected void update_meteo ()
	{
		double temp = temperatureSensor.get_currentValue ();
		double hum = humiditySensor.get_currentValue ();
		double pres = pressureSensor.get_currentValue ();

		myText.text = String.Format ("There is a Yocto-Meteo with the serial \"{0}\" connected.\n"
			+ "The ambient temperature is {1} degree Celsius, the humidity is {2}% and the atmospheric pressure is {3} mbar."
			+ "\n\nPress Space to continue or Esc to Exit.",
		                             meteoSerial, temp, hum, pres);
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			state = State.TEST_COLOR;
		}

	}

	protected int colorhue = 0;


	protected void update_color ()
	{
		myText.text = String.Format ("There is a Yocto-Color with the serial \"{0}\" connected. You can change the Hue of the leds with the Up and Down arrow."
			+ "\n\nPress Space to continue or Esc to Exit.",
		                             colorSerial);
		led1.set_hslColor ((colorhue << 16) + 0xff80);
		led2.set_hslColor ((colorhue << 16) + 0xff80);

		if (Input.GetKey (KeyCode.UpArrow)) {
			colorhue ++;
			if (colorhue > 0xff) {
				colorhue = 0;
			}
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			colorhue --;
			if (colorhue < 0) {
				colorhue = 0xff;
			}
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			state = State.HOME;
		}

	}
	protected void update_error ()
	{
		myText.text = "Oup's! Something bad happened:" + errmsg + "\n\nPress Space or Esc to Exit";
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Space)) {
			Application.Quit ();
		}

	}
}
