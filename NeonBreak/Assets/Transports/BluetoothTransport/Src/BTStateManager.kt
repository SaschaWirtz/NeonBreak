package de.MGD.NeonBreak.transports.bluetoothTransport;

import android.bluetooth.BluetoothAdapter;

/**
 * BTStateManager
 *
 * Class containing functions required for managing the
 * overall bluetooth state.
 */
public class BTStateManager {
    /**
     * Turns on the bluetooth functionality if existing.
     */ 
    public fun TurnOnBluetooth(): Int {
        var bluetoothAdapter: BluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

        if(bluetoothAdapter != null) {
            if(!bluetoothAdapter.isEnabled()) {
                bluetoothAdapter.enable();
                return 0;
            }else {
                return 0;
            }
        }
        return 1;
    }
}