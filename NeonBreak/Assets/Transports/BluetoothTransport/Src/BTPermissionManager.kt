package de.MGD.NeonBreak.transports.bluetoothTransport;

import androidx.activity.result.contract.ActivityResultContracts;
import androidx.core.content.ContextCompat;
import android.os.Build;
import androidx.core.app.ActivityCompat;
import androidx.appcompat.app.AppCompatActivity;
import android.Manifest;
import android.content.pm.PackageManager;

/**
 * BTPermissionManager
 *
 * Class with useful functions regarding the permissions
 * management.
 */
public class BTPermissionManager {
    /**
     * Checking if permission request would be required due to
     * backwards compatibility with older android devices.
     */
    public fun IsPermissionRequired(activity: AppCompatActivity): Boolean {
        if (ContextCompat.checkSelfPermission(activity, Manifest.permission.BLUETOOTH_CONNECT) == PackageManager.PERMISSION_DENIED) {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.S) {
                return true;
            }
        }
        return false;
    }
}