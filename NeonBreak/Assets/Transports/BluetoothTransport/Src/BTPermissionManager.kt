package de.MGD.NeonBreak.transports.bluetoothTransport;

import androidx.activity.result.contract.ActivityResultContracts;
import androidx.core.content.ContextCompat;
import android.os.Build;
import androidx.core.app.ActivityCompat;
import androidx.appcompat.app.AppCompatActivity;
import android.Manifest;
import android.content.pm.PackageManager;

public class BTPermissionManager {
    public fun RegisterPermissionListener(activity: AppCompatActivity) {
        val requestPermissionLauncher = activity.registerForActivityResult(
            ActivityResultContracts.RequestPermission()
        ) { isGranted: Boolean -> 
            if (isGranted) {
                BTStateManager().TurnOnBluetooth();
            }
        }
    }

    public fun RequestBluetoothPermissionIfRequired(activity: AppCompatActivity) {
        if (ContextCompat.checkSelfPermission(activity, Manifest.permission.BLUETOOTH_CONNECT) == PackageManager.PERMISSION_DENIED) {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.S) {
                ActivityCompat.requestPermissions(activity, arrayOf(Manifest.permission.BLUETOOTH_CONNECT), 2);
                return;
            }
        }
    }

    public fun IsPermissionRequired(activity: AppCompatActivity): Boolean {
        if (ContextCompat.checkSelfPermission(activity, Manifest.permission.BLUETOOTH_CONNECT) == PackageManager.PERMISSION_DENIED) {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.S) {
                // ActivityCompat.requestPermissions(activity, arrayOf(Manifest.permission.BLUETOOTH_CONNECT), 2);
                return true;
            }
        }
        return false;
    }
}