package de.MGD.NeonBreak.transports.bluetoothTransport;

import android.bluetooth.BluetoothAdapter;

public class BTStateManager { 
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