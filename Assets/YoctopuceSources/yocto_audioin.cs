/*********************************************************************
 *
 * $Id: pic24config.php 20732 2015-06-24 07:26:23Z mvuilleu $
 *
 * Implements yFindAudioIn(), the high-level API for AudioIn functions
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

    //--- (YAudioIn return codes)
    //--- (end of YAudioIn return codes)
//--- (YAudioIn dlldef)
//--- (end of YAudioIn dlldef)
//--- (YAudioIn class start)
/**
 * <summary>
 *   The Yoctopuce application programming interface allows you to configure the volume of the input channel.
 * <para>
 * </para>
 * <para>
 * </para>
 * </summary>
 */
public class YAudioIn : YFunction
{
//--- (end of YAudioIn class start)
    //--- (YAudioIn definitions)
    public new delegate void ValueCallback(YAudioIn func, string value);
    public new delegate void TimedReportCallback(YAudioIn func, YMeasure measure);

    public const int VOLUME_INVALID = YAPI.INVALID_UINT;
    public const int MUTE_FALSE = 0;
    public const int MUTE_TRUE = 1;
    public const int MUTE_INVALID = -1;
    public const string VOLUMERANGE_INVALID = YAPI.INVALID_STRING;
    public const int SIGNAL_INVALID = YAPI.INVALID_INT;
    public const int NOSIGNALFOR_INVALID = YAPI.INVALID_INT;
    protected int _volume = VOLUME_INVALID;
    protected int _mute = MUTE_INVALID;
    protected string _volumeRange = VOLUMERANGE_INVALID;
    protected int _signal = SIGNAL_INVALID;
    protected int _noSignalFor = NOSIGNALFOR_INVALID;
    protected ValueCallback _valueCallbackAudioIn = null;
    //--- (end of YAudioIn definitions)

    public YAudioIn(string func)
        : base(func)
    {
        _className = "AudioIn";
        //--- (YAudioIn attributes initialization)
        //--- (end of YAudioIn attributes initialization)
    }

    //--- (YAudioIn implementation)

    protected override void _parseAttr(YAPI.TJSONRECORD member)
    {
        if (member.name == "volume")
        {
            _volume = (int)member.ivalue;
            return;
        }
        if (member.name == "mute")
        {
            _mute = member.ivalue > 0 ? 1 : 0;
            return;
        }
        if (member.name == "volumeRange")
        {
            _volumeRange = member.svalue;
            return;
        }
        if (member.name == "signal")
        {
            _signal = (int)member.ivalue;
            return;
        }
        if (member.name == "noSignalFor")
        {
            _noSignalFor = (int)member.ivalue;
            return;
        }
        base._parseAttr(member);
    }

    /**
     * <summary>
     *   Returns audio input gain, in per cents.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   an integer corresponding to audio input gain, in per cents
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YAudioIn.VOLUME_INVALID</c>.
     * </para>
     */
    public int get_volume()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return VOLUME_INVALID;
            }
        }
        return this._volume;
    }

    /**
     * <summary>
     *   Changes audio input gain, in per cents.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <param name="newval">
     *   an integer corresponding to audio input gain, in per cents
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
    public int set_volume(int newval)
    {
        string rest_val;
        rest_val = (newval).ToString();
        return _setAttr("volume", rest_val);
    }

    /**
     * <summary>
     *   Returns the state of the mute function.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   either <c>YAudioIn.MUTE_FALSE</c> or <c>YAudioIn.MUTE_TRUE</c>, according to the state of the mute function
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YAudioIn.MUTE_INVALID</c>.
     * </para>
     */
    public int get_mute()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return MUTE_INVALID;
            }
        }
        return this._mute;
    }

    /**
     * <summary>
     *   Changes the state of the mute function.
     * <para>
     *   Remember to call the matching module
     *   <c>saveToFlash()</c> method to save the setting permanently.
     * </para>
     * <para>
     * </para>
     * </summary>
     * <param name="newval">
     *   either <c>YAudioIn.MUTE_FALSE</c> or <c>YAudioIn.MUTE_TRUE</c>, according to the state of the mute function
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
    public int set_mute(int newval)
    {
        string rest_val;
        rest_val = (newval > 0 ? "1" : "0");
        return _setAttr("mute", rest_val);
    }

    /**
     * <summary>
     *   Returns the supported volume range.
     * <para>
     *   The low value of the
     *   range corresponds to the minimal audible value. To
     *   completely mute the sound, use <c>set_mute()</c>
     *   instead of the <c>set_volume()</c>.
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a string corresponding to the supported volume range
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YAudioIn.VOLUMERANGE_INVALID</c>.
     * </para>
     */
    public string get_volumeRange()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return VOLUMERANGE_INVALID;
            }
        }
        return this._volumeRange;
    }

    /**
     * <summary>
     *   Returns the detected input signal level.
     * <para>
     * </para>
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   an integer corresponding to the detected input signal level
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YAudioIn.SIGNAL_INVALID</c>.
     * </para>
     */
    public int get_signal()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return SIGNAL_INVALID;
            }
        }
        return this._signal;
    }

    /**
     * <summary>
     *   Returns the number of seconds elapsed without detecting a signal
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   an integer corresponding to the number of seconds elapsed without detecting a signal
     * </returns>
     * <para>
     *   On failure, throws an exception or returns <c>YAudioIn.NOSIGNALFOR_INVALID</c>.
     * </para>
     */
    public int get_noSignalFor()
    {
        if (this._cacheExpiration <= YAPI.GetTickCount()) {
            if (this.load(YAPI.DefaultCacheValidity) != YAPI.SUCCESS) {
                return NOSIGNALFOR_INVALID;
            }
        }
        return this._noSignalFor;
    }

    /**
     * <summary>
     *   Retrieves an audio input for a given identifier.
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
     *   This function does not require that the audio input is online at the time
     *   it is invoked. The returned object is nevertheless valid.
     *   Use the method <c>YAudioIn.isOnline()</c> to test if the audio input is
     *   indeed online at a given time. In case of ambiguity when looking for
     *   an audio input by logical name, no error is notified: the first instance
     *   found is returned. The search is performed first by hardware name,
     *   then by logical name.
     * </para>
     * </summary>
     * <param name="func">
     *   a string that uniquely characterizes the audio input
     * </param>
     * <returns>
     *   a <c>YAudioIn</c> object allowing you to drive the audio input.
     * </returns>
     */
    public static YAudioIn FindAudioIn(string func)
    {
        YAudioIn obj;
        obj = (YAudioIn) YFunction._FindFromCache("AudioIn", func);
        if (obj == null) {
            obj = new YAudioIn(func);
            YFunction._AddToCache("AudioIn", func, obj);
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
        this._valueCallbackAudioIn = callback;
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
        if (this._valueCallbackAudioIn != null) {
            this._valueCallbackAudioIn(this, value);
        } else {
            base._invokeValueCallback(value);
        }
        return 0;
    }

    /**
     * <summary>
     *   Continues the enumeration of audio inputs started using <c>yFirstAudioIn()</c>.
     * <para>
     * </para>
     * </summary>
     * <returns>
     *   a pointer to a <c>YAudioIn</c> object, corresponding to
     *   an audio input currently online, or a <c>null</c> pointer
     *   if there are no more audio inputs to enumerate.
     * </returns>
     */
    public YAudioIn nextAudioIn()
    {
        string hwid = "";
        if (YAPI.YISERR(_nextFunction(ref hwid)))
            return null;
        if (hwid == "")
            return null;
        return FindAudioIn(hwid);
    }

    //--- (end of YAudioIn implementation)

    //--- (AudioIn functions)

    /**
     * <summary>
     *   Starts the enumeration of audio inputs currently accessible.
     * <para>
     *   Use the method <c>YAudioIn.nextAudioIn()</c> to iterate on
     *   next audio inputs.
     * </para>
     * </summary>
     * <returns>
     *   a pointer to a <c>YAudioIn</c> object, corresponding to
     *   the first audio input currently online, or a <c>null</c> pointer
     *   if there are none.
     * </returns>
     */
    public static YAudioIn FirstAudioIn()
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
        err = YAPI.apiGetFunctionsByClass("AudioIn", 0, p, size, ref neededsize, ref errmsg);
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
        return FindAudioIn(serial + "." + funcId);
    }



    //--- (end of AudioIn functions)
}
