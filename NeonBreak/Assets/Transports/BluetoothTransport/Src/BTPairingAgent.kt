package de.MGD.NeonBreak.transports.bluetoothTransport;

import java.util.Set
import android.bluetooth.BluetoothDevice
import android.content.BroadcastReceiver
import android.content.IntentFilter
import android.content.Intent
import android.content.Context
import android.bluetooth.BluetoothAdapter
import androidx.appcompat.app.AppCompatActivity
import android.util.Log;

/**
 * BTPairingAgent
 *
 * Class containing functions helping with the device pairing
 * and pairing recognition.
 */
// class BTPairingAgent() {
//     /**
//      * Method registering the receiver on the given activity
//      *
//      * @param activity The activity to be used for registration
//      */
//     fun registerReceiver(activity: AppCompatActivity) {
//         val filter = IntentFilter(BluetoothDevice.ACTION_FOUND)
//         activity.registerReceiver(receiver, filter)
//     }

//     /**
//      * Method reverting the current registration of the receiver
//      * for the given activity
//      *
//      * @param activity The activity to remove the receiver from
//      */
//     fun unregisterReceiver(activity: AppCompatActivity) {
//         activity.unregisterReceiver(receiver)
//     }

//     /**
//      * Starting the discovery process and clearing the list
//      * of discovered devices.
//      */
//     fun startDiscovery() {
//         this.discoveredDevices.clear();
//         BluetoothAdapter.getDefaultAdapter().startDiscovery();
//     }

//     /**
//      * Searching for the device with given name and return the
//      * devices mac address
//      *
//      * @param name The name to search the MAC for
//      * @return The MAC address or null
//      */
//     fun getMacForDeviceName(name: String): String? {
//         val deviceWithName = this.discoveredDevices.filter {it.getName() == name};

//         if (deviceWithName.size < 1) {
//             return null;
//         } else {
//             return deviceWithName.elementAt(0).getAddress();
//         }
//     }
// }
