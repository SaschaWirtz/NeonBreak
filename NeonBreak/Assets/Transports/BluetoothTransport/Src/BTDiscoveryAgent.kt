package de.MGD.NeonBreak.transports.bluetoothTransport;

import java.util.Set
import android.bluetooth.BluetoothDevice
import android.content.BroadcastReceiver
import android.content.IntentFilter
import android.content.Intent
import android.content.Context
import android.bluetooth.BluetoothAdapter
import androidx.appcompat.app.AppCompatActivity


class BTDiscoveryAgent() {
    // Create a BroadcastReceiver for ACTION_FOUND.
    private val receiver = object : BroadcastReceiver() {

        override fun onReceive(context: Context, intent: Intent) {
            val action: String = intent.action!!
            when(action) {
                BluetoothDevice.ACTION_FOUND -> {
                    // Discovery has found a device. Get the BluetoothDevice
                    // object and its info from the Intent.
                    val device: BluetoothDevice =
                    intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE)!!
                    if(!BTDiscoveryAgent.discoveredDevices.contains(device)) {
                        BTDiscoveryAgent.discoveredDevices.add(device)
                    }
                }
            }
        }
    }

    fun registerReceiver(activity: AppCompatActivity) {
        val filter = IntentFilter(BluetoothDevice.ACTION_FOUND)
        activity.registerReceiver(receiver, filter)
    }

    fun unregisterReceiver(activity: AppCompatActivity) {
        activity.unregisterReceiver(receiver)
    }

    fun startDiscovery() {
        BluetoothAdapter.getDefaultAdapter().startDiscovery();
    }

    companion object {
        val discoveredDevices = mutableSetOf<BluetoothDevice>()

        fun getNameById(id: Int): String {
            return discoveredDevices.elementAt(id).getName();
        }

        fun getMacById(id: Int): String {
            return discoveredDevices.elementAt(id).getAddress();
        }

        fun getSize(): Int {
            return discoveredDevices.size;
        }
    }
}
