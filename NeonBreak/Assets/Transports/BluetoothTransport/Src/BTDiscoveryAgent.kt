package de.MGD.NeonBreak.transports.bluetoothTransport;

import java.util.Set
import android.bluetooth.BluetoothDevice
import android.content.BroadcastReceiver
import android.content.IntentFilter
import android.content.Intent
import android.content.Context
import android.bluetooth.BluetoothAdapter
import androidx.appcompat.app.AppCompatActivity

/**
 * BTDiscoveryAgent
 *
 * Class containing functions helping with the device discovery
 * and MAC detection.
 */
class BTDiscoveryAgent() {
    /**
     * A list of discovered devices.
     */
    private val discoveredDevices = mutableSetOf<BluetoothDevice>()

    /**
     * A broadcast receiver for the BT device found action
     * filling a device list.
     */
    private val receiver = object : BroadcastReceiver() {
        /**
         * Override function executed after receiving a system broadcast.
         *
         * @param context The android context received from broadcast
         * @param intent The intent received from broadcast
         */
        override fun onReceive(context: Context, intent: Intent) {
            val action: String = intent.action!!
            when(action) {
                BluetoothDevice.ACTION_FOUND -> {
                    val device: BluetoothDevice =
                    intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE)!!
                    if(!discoveredDevices.contains(device)) {
                        discoveredDevices.add(device)
                    }
                }
            }
        }
    }

    /**
     * Method registering the receiver on the given activity
     *
     * @param activity The activity to be used for registration
     */
    fun registerReceiver(activity: AppCompatActivity) {
        val filter = IntentFilter(BluetoothDevice.ACTION_FOUND)
        activity.registerReceiver(receiver, filter)
    }

    /**
     * Method reverting the current registration of the receiver
     * for the given activity
     *
     * @param activity The activity to remove the receiver from
     */
    fun unregisterReceiver(activity: AppCompatActivity) {
        activity.unregisterReceiver(receiver)
    }

    /**
     * Starting the discovery process and clearing the list
     * of discovered devices.
     */
    fun startDiscovery() {
        this.discoveredDevices.clear();
        BluetoothAdapter.getDefaultAdapter().startDiscovery();
    }

    /**
     * Searching for the device with given name and return the
     * devices mac address
     *
     * @param name The name to search the MAC for
     * @return The device object or null
     */
    fun getDeviceByName(name: String): BluetoothDevice? {
        val deviceWithName = this.discoveredDevices.filter {it.getName() == name};

        if (deviceWithName.size < 1) {
            return null;
        } else {
            return deviceWithName.elementAt(0).getAddress();
        }
    }
}
