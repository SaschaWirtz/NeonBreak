package de.MGD.NeonBreak.transports.bluetoothTransport;

import android.bluetooth.BluetoothAdapter
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import java.lang.Thread

/**
 * The BTDiscoverStateManager
 *
 * Used to manage the current discoverability state of the current
 * bluetooth adapter.
 */
public class BTDiscoverStateManager { 
    /**
     * Changes the current bluetooth adapter of device to 
     * discoverable mode.
     *
     * @param activity The activity to be used for intent creation
     */
    public fun makeDiscoverable(activity: AppCompatActivity) {
        val requestCode = 4;
        val discoverableIntent: Intent = Intent(BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE).apply {
            putExtra(BluetoothAdapter.EXTRA_DISCOVERABLE_DURATION, 300)
        }
        activity.startActivityForResult(discoverableIntent, requestCode)
    }

    /**
     * Stops the bluetooth discoverability mode by disabling
     * and reenabling the bluetooth adapter for the current
     * device.
     */
    public fun stopDiscoverableMode() {
        if (BluetoothAdapter.getDefaultAdapter().isEnabled()) {
            BluetoothAdapter.getDefaultAdapter().disable();

            while (BluetoothAdapter.getDefaultAdapter().isEnabled()) {
                Thread.sleep(100);
            }

            BluetoothAdapter.getDefaultAdapter().enable();
        }
    }
}