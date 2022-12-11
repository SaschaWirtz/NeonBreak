package de.MGD.NeonBreak.transports.bluetoothTransport;

import android.bluetooth.BluetoothAdapter
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity

public class BTDiscoverStateManager { 
    public fun makeDiscoverable(activity: AppCompatActivity) {
        val requestCode = 4;
        val discoverableIntent: Intent = Intent(BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE).apply {
            putExtra(BluetoothAdapter.EXTRA_DISCOVERABLE_DURATION, 300)
        }
        activity.startActivityForResult(discoverableIntent, requestCode)
    }
}