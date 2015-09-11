/*********************************************************************
 *
 * $Id: pic24config.php 20732 2015-06-24 07:26:23Z mvuilleu $
 *
 * Implements yFindMagnetometer(), the high-level API for Magnetometer functions
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

    //--- (YMagnetometer return codes)
    //--- (end of YMagnetometer return codes)
//--- (YMagnetometer dlldef)
//--- (end of YMagnetometer dlldef)
//--- (YMagnetometer class start)
/**
 * <summary>
 *   The YSensor class is the parent class for all Yoctopuce sensors.
 * <para>
 *   It can be
 *   used to read the current value and unit of any sensor, read the min/max
 *   value, configure autonomous recording frequency and access recorded data.
 *   It also provide a function to register a callback invoked each time the
 *   observed value changes, or at a predefined interval. Using this class rather
 *   than a specific subclass makes it possible to create generic applications
 *   that work with any Yoctopuce sensor, even those that do not yet exist.
 *   Note: The YAnButton class is the only analog input which does not inherit
 *   from YSensor.
 * </para>
 * <para>
 * </para>
 * </summary>
 */
public class YMagnetometer : YSensor
{
//--- (end of YMagnetometer class start)
    //--- (YMagnetometer definitions)
    public new delegate void ValueCallback(YMagnetometer func, string value);
    public new delegate void TimedReportCallback(YMagnetometer func, YMeasure measure);

    public const double XVALUE_INVALID = YAPI.INVALID_DOUBLE;
    public const double YVALUE_INVALID = YAPI.INVALID_DOUBLE;
    public const double ZVALUE_INVALID = YAPI.INVALID_DOUBLE;
    protected double _xValue = XVALUE_INVALID;
    protected double _yValue = YVALUE_INVALID;
    protected double _zValue = ZVALUE_INVALID;
    protected ValueCallback _valueCallbackMagnetometer = null;
    protected TimedReportCallback _timedReportCallbackMagnetometer = null;
    //--- (end of YMagnetometer definitions)

    public YMagnetometer(string func)
        : base(func)
    {
        _className = "Magnetometer";
        //--- (YMagnetometer attributes initialization)
        //--- (end of YMagnetometer attributes initialization)
    }

    //--- (YMagnetometer implementation)

    protected override void _parseAttr(YAPI.TJSONRECORD member)
    {
        if (member.name == "xValue")
        {
            _xValue = Math.Round(member.ivalue * 1000.0 / 65536.0) / 1000.0;
            return;
        }
        if (member.name == "yValue")
        {
            _yValue = Math.Round(member.ivalue * 1000.0 / 65536.0) / 1000.0;
            return;
        }
        if (member.name == "zValue")
        {
            _zValue = Math.Round(member.ivalue * 1000.0 / 65536.0) / 1000.0;
            return;
        }
        base._parseAttr(member);
    }

    /**
     * <summary>
     *   Returns the X component of the magnetic field, as a floating point number.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a floating point number corresponding to the X component of the magnetic field, as a floating point number
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YMagnetometer.XVALUE_INVALID</c>.
     * </para>
     */
    public double get_xValue()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return XVALUE_INVALID;
            }
        }
        return this._xValue;
    }

    /**
     * <summary>
     *   Returns the Y component of the magnetic field, as a floating point number.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a floating point number corresponding to the Y component of the magnetic field, as a floating point number
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YMagnetometer.YVALUE_INVALID</c>.
     * </para>
     */
    public double get_yValue()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return YVALUE_INVALID;
            }
        }
        return this._yValue;
    }

    /**
     * <summary>
     *   Returns the Z component of the magnetic field, as a floating point number.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a floating point number corresponding to the Z component of the magnetic field, as a floating point number
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YMagnetometer.ZVALUE_INVALID</c>.
     * </para>
     */
    public double get_zValue()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return ZVALUE_INVALID;
            }
        }
        return this._zValue;
    }

    /**
     * <summary>
     *   Retrieves a magnetometer for a given identifier.
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
     *   This function does not require that the magnetometer is online at the time
     *   it is invoked. The returned object is nevertheless valid.
     *   Use the method <c>YMagnetometer.isOnline()</c> to test if the magnetometer is
     *   indeed online at a given time. In case of ambiguity when looking for
     *   a magnetometer by logical name, no error is notified: the first instance
     *   found is returned. The search is performed first by hardware name,
     *   then by logical name.
     * </para>
     * </summary>
     * <param name="func">
     *   a string that uniquely characterizes the magnetometer
     * </param>
     * <returns>
     *   a <c>YMagnetometer</c> object allowing you to drive the magnetometer.
     * </returns>
     */
    public static YMagnetometer FindMagnetometer(string func)
    {
        YMagnetometer obj;
        obj = (YMagnetometer) YFunction._FindFromCache("Magnetometer", func);
        if (obj == null) {
            obj = new YMagnetometer(func);
            YFunction._AddToCache("Magnetometer", func, obj);
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
        this._valueCallbackMagnetometer = callback;
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
        if (this._valueCallbackMagnetometer != null) {
            this._valueCallbackMagnetometer(this, value);
        } else {
            base._invokeValueCallback(value);
        }
        return 0;
    }

    /**
     * <summary>
     *   Registers the callback function that is invoked on every periodic timed notification.
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
     *   arguments: the function object of which the value has changed, and an YMeasure object describing
     *   the new advertised value.
     * @noreturn
     * </param>
     */
    public int registerTimedReportCallback(TimedReportCallback callback)
    {
        if (callback != null) {
            YFunction._UpdateTimedReportCallbackList(this, true);
        } else {
            YFunction._UpdateTimedReportCallbackList(this, false);
        }
        this._timedReportCallbackMagnetometer = callback;
        return 0;
    }

    public override int _invokeTimedReportCallback(YMeasure value)
    {
        if (this._timedReportCallbackMagnetometer != null) {
            this._timedReportCallbackMagnetometer(this, value);
        } else {
            base._invokeTimedReportCallback(value);
        }
        return 0;
    }

    /**
     * <summary>
     *   Continues the enumeration of magnetometers started using <c>yFirstMagnetometer()</c>.
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a pointer to a <c>YMagnetometer</c> object, corresponding to
     *   a magnetometer currently online, or a <c>null</c> pointer
     *   if there are no more magnetometers to enumerate.
     * </returns>
     */
    public YMagnetometer nextMagnetometer()
    {
        string hwid = "";
        if (YAPI.YISERR(_nextFunction(ref hwid)))
            return null;
        if (hwid == "")
            return null;
        return FindMagnetometer(hwid);
    }

    //--- (end of YMagnetometer implementation)

    //--- (Magnetometer functions)

    /**
     * <summary>
     *   Starts the enumeration of magnetometers currently accessible.
     * <para>
     *   Use the method <c>YMagnetometer.nextMagnetometer()</c> to iterate on
     *   next magnetometers.
     * </para>
     * </summary>
     * <returns>
     *   a pointer to a <c>YMagnetometer</c> object, corresponding to
     *   the first magnetometer currently online, or a <c>null</c> pointer
     *   if there are none.
     * </returns>
     */
    public static YMagnetometer FirstMagnetometer()
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
        err = YAPI.apiGetFunctionsByClass("Magnetometer", 0, p, size, ref neededsize, ref errmsg);
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
        return FindMagnetometer(serial + "." + funcId);
    }



    //--- (end of Magnetometer functions)
}
