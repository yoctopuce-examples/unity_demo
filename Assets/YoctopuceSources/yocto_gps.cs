/*********************************************************************
 *
 * $Id: pic24config.php 20732 2015-06-24 07:26:23Z mvuilleu $
 *
 * Implements yFindGps(), the high-level API for Gps functions
 *
 * - - - - - - - - - License information: - - - - - - - - - 
 *
 *  Copyright (C) 2011 and beyond by Yoctopuce Sarl, Switzerland.
 *
 *  Yoctopuce Sarl (hereafter Licensor) grants to you a perpetual
 *  non-exclusive license to use, modify, copy and integrate this
 *  file into your software for the sole purpose of interfacing
 *  with Yoctopuce products.
 *
 *  You may reproduce and distribute copies of this file in
 *  source or object form, as long as the sole purpose of this
 *  code is to interface with Yoctopuce products. You must retain
 *  this notice in the distributed source file.
 *
 *  You should refer to Yoctopuce General Terms and Conditions
 *  for additional information regarding your rights and
 *  obligations.
 *
 *  THE SOFTWARE AND DOCUMENTATION ARE PROVIDED 'AS IS' WITHOUT
 *  WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING 
 *  WITHOUT LIMITATION, ANY WARRANTY OF MERCHANTABILITY, FITNESS
 *  FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO
 *  EVENT SHALL LICENSOR BE LIABLE FOR ANY INCIDENTAL, SPECIAL,
 *  INDIRECT OR CONSEQUENTIAL DAMAGES, LOST PROFITS OR LOST DATA,
 *  COST OF PROCUREMENT OF SUBSTITUTE GOODS, TECHNOLOGY OR 
 *  SERVICES, ANY CLAIMS BY THIRD PARTIES (INCLUDING BUT NOT 
 *  LIMITED TO ANY DEFENSE THEREOF), ANY CLAIMS FOR INDEMNITY OR
 *  CONTRIBUTION, OR OTHER SIMILAR COSTS, WHETHER ASSERTED ON THE
 *  BASIS OF CONTRACT, TORT (INCLUDING NEGLIGENCE), BREACH OF
 *  WARRANTY, OR OTHERWISE.
 *
 *********************************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using YDEV_DESCR = System.Int32;
using YFUN_DESCR = System.Int32;

    //--- (YGps return codes)
    //--- (end of YGps return codes)
//--- (YGps dlldef)
//--- (end of YGps dlldef)
//--- (YGps class start)
/**
 * <summary>
 *   The Gps function allows you to extract positionning
 *   data from the GPS device.
 * <para>
 *   This class can provides
 *   complete positionning information: However, if you
 *   whish to define callbacks on position changes, you
 *   should use the YLatitude et YLongitude classes.
 * </para>
 * <para>
 * </para>
 * </summary>
 */
public class YGps : YFunction
{
//--- (end of YGps class start)
    //--- (YGps definitions)
    public new delegate void ValueCallback(YGps func, string value);
    public new delegate void TimedReportCallback(YGps func, YMeasure measure);

    public const int ISFIXED_FALSE = 0;
    public const int ISFIXED_TRUE = 1;
    public const int ISFIXED_INVALID = -1;
    public const long SATCOUNT_INVALID = YAPI.INVALID_LONG;
    public const int COORDSYSTEM_GPS_DMS = 0;
    public const int COORDSYSTEM_GPS_DM = 1;
    public const int COORDSYSTEM_GPS_D = 2;
    public const int COORDSYSTEM_INVALID = -1;
    public const string LATITUDE_INVALID = YAPI.INVALID_STRING;
    public const string LONGITUDE_INVALID = YAPI.INVALID_STRING;
    public const double DILUTION_INVALID = YAPI.INVALID_DOUBLE;
    public const double ALTITUDE_INVALID = YAPI.INVALID_DOUBLE;
    public const double GROUNDSPEED_INVALID = YAPI.INVALID_DOUBLE;
    public const double DIRECTION_INVALID = YAPI.INVALID_DOUBLE;
    public const long UNIXTIME_INVALID = YAPI.INVALID_LONG;
    public const string DATETIME_INVALID = YAPI.INVALID_STRING;
    public const int UTCOFFSET_INVALID = YAPI.INVALID_INT;
    public const string COMMAND_INVALID = YAPI.INVALID_STRING;
    protected int _isFixed = ISFIXED_INVALID;
    protected long _satCount = SATCOUNT_INVALID;
    protected int _coordSystem = COORDSYSTEM_INVALID;
    protected string _latitude = LATITUDE_INVALID;
    protected string _longitude = LONGITUDE_INVALID;
    protected double _dilution = DILUTION_INVALID;
    protected double _altitude = ALTITUDE_INVALID;
    protected double _groundSpeed = GROUNDSPEED_INVALID;
    protected double _direction = DIRECTION_INVALID;
    protected long _unixTime = UNIXTIME_INVALID;
    protected string _dateTime = DATETIME_INVALID;
    protected int _utcOffset = UTCOFFSET_INVALID;
    protected string _command = COMMAND_INVALID;
    protected ValueCallback _valueCallbackGps = null;
    //--- (end of YGps definitions)

    public YGps(string func)
        : base(func)
    {
        _className = "Gps";
        //--- (YGps attributes initialization)
        //--- (end of YGps attributes initialization)
    }

    //--- (YGps implementation)

    protected override void _parseAttr(YAPI.TJSONRECORD member)
    {
        if (member.name == "isFixed")
        {
            _isFixed = member.ivalue > 0 ? 1 : 0;
            return;
        }
        if (member.name == "satCount")
        {
            _satCount = member.ivalue;
            return;
        }
        if (member.name == "coordSystem")
        {
            _coordSystem = (int)member.ivalue;
            return;
        }
        if (member.name == "latitude")
        {
            _latitude = member.svalue;
            return;
        }
        if (member.name == "longitude")
        {
            _longitude = member.svalue;
            return;
        }
        if (member.name == "dilution")
        {
            _dilution = Math.Round(member.ivalue * 1000.0 / 65536.0) / 1000.0;
            return;
        }
        if (member.name == "altitude")
        {
            _altitude = Math.Round(member.ivalue * 1000.0 / 65536.0) / 1000.0;
            return;
        }
        if (member.name == "groundSpeed")
        {
            _groundSpeed = Math.Round(member.ivalue * 1000.0 / 65536.0) / 1000.0;
            return;
        }
        if (member.name == "direction")
        {
            _direction = Math.Round(member.ivalue * 1000.0 / 65536.0) / 1000.0;
            return;
        }
        if (member.name == "unixTime")
        {
            _unixTime = member.ivalue;
            return;
        }
        if (member.name == "dateTime")
        {
            _dateTime = member.svalue;
            return;
        }
        if (member.name == "utcOffset")
        {
            _utcOffset = (int)member.ivalue;
            return;
        }
        if (member.name == "command")
        {
            _command = member.svalue;
            return;
        }
        base._parseAttr(member);
    }

    /**
     * <summary>
     *   Returns TRUE if the receiver has found enough satellites to work
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   either <c>YGps.ISFIXED_FALSE</c> or <c>YGps.ISFIXED_TRUE</c>, according to TRUE if the receiver has
     *   found enough satellites to work
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.ISFIXED_INVALID</c>.
     * </para>
     */
    public int get_isFixed()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return ISFIXED_INVALID;
            }
        }
        return this._isFixed;
    }

    /**
     * <summary>
     *   Returns the count of visible satellites.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   an integer corresponding to the count of visible satellites
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.SATCOUNT_INVALID</c>.
     * </para>
     */
    public long get_satCount()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return SATCOUNT_INVALID;
            }
        }
        return this._satCount;
    }

    /**
     * <summary>
     *   Returns the representation system used for positioning data.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a value among <c>YGps.COORDSYSTEM_GPS_DMS</c>, <c>YGps.COORDSYSTEM_GPS_DM</c> and
     *   <c>YGps.COORDSYSTEM_GPS_D</c> corresponding to the representation system used for positioning data
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.COORDSYSTEM_INVALID</c>.
     * </para>
     */
    public int get_coordSystem()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return COORDSYSTEM_INVALID;
            }
        }
        return this._coordSystem;
    }

    /**
     * <summary>
     *   Changes the representation system used for positioning data.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <param name="newval">
     *   a value among <c>YGps.COORDSYSTEM_GPS_DMS</c>, <c>YGps.COORDSYSTEM_GPS_DM</c> and
     *   <c>YGps.COORDSYSTEM_GPS_D</c> corresponding to the representation system used for positioning data
     * </param>
     * <para>
     * </para>
     * <returns>
     *   <c>YAPI.SUCCESS</c> if the call succeeds.
     * </returns>
     * <para>
     *   On failure, throws an exception or returns a negative error code.
     * </para>
     */
    public int set_coordSystem(int newval)
    {
        string rest_val;
        rest_val = (newval).ToString();
        return _setAttr("coordSystem", rest_val);
    }

    /**
     * <summary>
     *   Returns the current latitude.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a string corresponding to the current latitude
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.LATITUDE_INVALID</c>.
     * </para>
     */
    public string get_latitude()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return LATITUDE_INVALID;
            }
        }
        return this._latitude;
    }

    /**
     * <summary>
     *   Returns the current longitude.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a string corresponding to the current longitude
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.LONGITUDE_INVALID</c>.
     * </para>
     */
    public string get_longitude()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return LONGITUDE_INVALID;
            }
        }
        return this._longitude;
    }

    /**
     * <summary>
     *   Returns the current horizontal dilution of precision,
     *   the smaller that number is, the better .
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a floating point number corresponding to the current horizontal dilution of precision,
     *   the smaller that number is, the better
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.DILUTION_INVALID</c>.
     * </para>
     */
    public double get_dilution()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return DILUTION_INVALID;
            }
        }
        return this._dilution;
    }

    /**
     * <summary>
     *   Returns the current altitude.
     * <para>
     *   Beware:  GPS technology
     *   is very inaccurate regarding altitude.
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a floating point number corresponding to the current altitude
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.ALTITUDE_INVALID</c>.
     * </para>
     */
    public double get_altitude()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return ALTITUDE_INVALID;
            }
        }
        return this._altitude;
    }

    /**
     * <summary>
     *   Returns the current ground speed in Km/h.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a floating point number corresponding to the current ground speed in Km/h
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.GROUNDSPEED_INVALID</c>.
     * </para>
     */
    public double get_groundSpeed()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return GROUNDSPEED_INVALID;
            }
        }
        return this._groundSpeed;
    }

    /**
     * <summary>
     *   Returns the current move bearing in degrees, zero
     *   is the true (geographic) north.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a floating point number corresponding to the current move bearing in degrees, zero
     *   is the true (geographic) north
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.DIRECTION_INVALID</c>.
     * </para>
     */
    public double get_direction()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return DIRECTION_INVALID;
            }
        }
        return this._direction;
    }

    /**
     * <summary>
     *   Returns the current time in Unix format (number of
     *   seconds elapsed since Jan 1st, 1970).
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   an integer corresponding to the current time in Unix format (number of
     *   seconds elapsed since Jan 1st, 1970)
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.UNIXTIME_INVALID</c>.
     * </para>
     */
    public long get_unixTime()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return UNIXTIME_INVALID;
            }
        }
        return this._unixTime;
    }

    /**
     * <summary>
     *   Returns the current time in the form "YYYY/MM/DD hh:mm:ss"
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a string corresponding to the current time in the form "YYYY/MM/DD hh:mm:ss"
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.DATETIME_INVALID</c>.
     * </para>
     */
    public string get_dateTime()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return DATETIME_INVALID;
            }
        }
        return this._dateTime;
    }

    /**
     * <summary>
     *   Returns the number of seconds between current time and UTC time (time zone).
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   an integer corresponding to the number of seconds between current time and UTC time (time zone)
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YGps.UTCOFFSET_INVALID</c>.
     * </para>
     */
    public int get_utcOffset()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return UTCOFFSET_INVALID;
            }
        }
        return this._utcOffset;
    }

    /**
     * <summary>
     *   Changes the number of seconds between current time and UTC time (time zone).
     * <para>
     *   The timezone is automatically rounded to the nearest multiple of 15 minutes.
     *   If current UTC time is known, the current time is automatically be updated according to the selected time zone.
     * </para>
     * <para>
     * </para>
     * </summary>
     * <param name="newval">
     *   an integer corresponding to the number of seconds between current time and UTC time (time zone)
     * </param>
     * <para>
     * </para>
     * <returns>
     *   <c>YAPI.SUCCESS</c> if the call succeeds.
     * </returns>
     * <para>
     *   On failure, throws an exception or returns a negative error code.
     * </para>
     */
    public int set_utcOffset(int newval)
    {
        string rest_val;
        rest_val = (newval).ToString();
        return _setAttr("utcOffset", rest_val);
    }

    public string get_command()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return COMMAND_INVALID;
            }
        }
        return this._command;
    }

    public int set_command(string newval)
    {
        string rest_val;
        rest_val = newval;
        return _setAttr("command", rest_val);
    }

    /**
     * <summary>
     *   Retrieves a GPS for a given identifier.
     * <para>
     *   The identifier can be specified using several formats:
     * </para>
     * <para>
     * </para>
     * <para>
     *   - FunctionLogicalName
     * </para>
     * <para>
     *   - ModuleSerialNumber.FunctionIdentifier
     * </para>
     * <para>
     *   - ModuleSerialNumber.FunctionLogicalName
     * </para>
     * <para>
     *   - ModuleLogicalName.FunctionIdentifier
     * </para>
     * <para>
     *   - ModuleLogicalName.FunctionLogicalName
     * </para>
     * <para>
     * </para>
     * <para>
     *   This function does not require that the GPS is online at the time
     *   it is invoked. The returned object is nevertheless valid.
     *   Use the method <c>YGps.isOnline()</c> to test if the GPS is
     *   indeed online at a given time. In case of ambiguity when looking for
     *   a GPS by logical name, no error is notified: the first instance
     *   found is returned. The search is performed first by hardware name,
     *   then by logical name.
     * </para>
     * </summary>
     * <param name="func">
     *   a string that uniquely characterizes the GPS
     * </param>
     * <returns>
     *   a <c>YGps</c> object allowing you to drive the GPS.
     * </returns>
     */
    public static YGps FindGps(string func)
    {
        YGps obj;
        obj = (YGps) YFunction._FindFromCache("Gps", func);
        if (obj == null) {
            obj = new YGps(func);
            YFunction._AddToCache("Gps", func, obj);
        }
        return obj;
    }

    /**
     * <summary>
     *   Registers the callback function that is invoked on every change of advertised value.
     * <para>
     *   The callback is invoked only during the execution of <c>ySleep</c> or <c>yHandleEvents</c>.
     *   This provides control over the time when the callback is triggered. For good responsiveness, remember to call
     *   one of these two functions periodically. To unregister a callback, pass a null pointer as argument.
     * </para>
     * <para>
     * </para>
     * </summary>
     * <param name="callback">
     *   the callback function to call, or a null pointer. The callback function should take two
     *   arguments: the function object of which the value has changed, and the character string describing
     *   the new advertised value.
     * @noreturn
     * </param>
     */
    public int registerValueCallback(ValueCallback callback)
    {
        string val;
        if (callback != null) {
            YFunction._UpdateValueCallbackList(this, true);
        } else {
            YFunction._UpdateValueCallbackList(this, false);
        }
        this._valueCallbackGps = callback;
        // Immediately invoke value callback with current value
        if (callback != null && this.isOnline()) {
            val = this._advertisedValue;
            if (!(val == "")) {
                this._invokeValueCallback(val);
            }
        }
        return 0;
    }

    public override int _invokeValueCallback(string value)
    {
        if (this._valueCallbackGps != null) {
            this._valueCallbackGps(this, value);
        } else {
            base._invokeValueCallback(value);
        }
        return 0;
    }

    /**
     * <summary>
     *   Continues the enumeration of GPS started using <c>yFirstGps()</c>.
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a pointer to a <c>YGps</c> object, corresponding to
     *   a GPS currently online, or a <c>null</c> pointer
     *   if there are no more GPS to enumerate.
     * </returns>
     */
    public YGps nextGps()
    {
        string hwid = "";
        if (YAPI.YISERR(_nextFunction(ref hwid)))
            return null;
        if (hwid == "")
            return null;
        return FindGps(hwid);
    }

    //--- (end of YGps implementation)

    //--- (Gps functions)

    /**
     * <summary>
     *   Starts the enumeration of GPS currently accessible.
     * <para>
     *   Use the method <c>YGps.nextGps()</c> to iterate on
     *   next GPS.
     * </para>
     * </summary>
     * <returns>
     *   a pointer to a <c>YGps</c> object, corresponding to
     *   the first GPS currently online, or a <c>null</c> pointer
     *   if there are none.
     * </returns>
     */
    public static YGps FirstGps()
    {
        YFUN_DESCR[] v_fundescr = new YFUN_DESCR[1];
        YDEV_DESCR dev = default(YDEV_DESCR);
        int neededsize = 0;
        int err = 0;
        string serial = null;
        string funcId = null;
        string funcName = null;
        string funcVal = null;
        string errmsg = "";
        int size = Marshal.SizeOf(v_fundescr[0]);
        IntPtr p = Marshal.AllocHGlobal(Marshal.SizeOf(v_fundescr[0]));
        err = YAPI.apiGetFunctionsByClass("Gps", 0, p, size, ref neededsize, ref errmsg);
        Marshal.Copy(p, v_fundescr, 0, 1);
        Marshal.FreeHGlobal(p);
        if ((YAPI.YISERR(err) | (neededsize == 0)))
            return null;
        serial = "";
        funcId = "";
        funcName = "";
        funcVal = "";
        errmsg = "";
        if ((YAPI.YISERR(YAPI.yapiGetFunctionInfo(v_fundescr[0], ref dev, ref serial, ref funcId, ref funcName, ref funcVal, ref errmsg))))
            return null;
        return FindGps(serial + "." + funcId);
    }



    //--- (end of Gps functions)
}
