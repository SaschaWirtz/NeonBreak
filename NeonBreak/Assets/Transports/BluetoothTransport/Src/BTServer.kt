package de.MGD.NeonBreak.transports.bluetoothTransport;

import java.lang.Thread;
import java.lang.Exception;
import android.bluetooth.BluetoothSocket;
import androidx.appcompat.app.AppCompatActivity;

class BTServer(private val activity: AppCompatActivity, private val socket: BluetoothSocket): Thread() {
    private val inputStream = this.socket.inputStream
    private val outputStream = this.socket.outputStream

    override fun run() {
        try {
            val available = inputStream.available()
            val bytes = ByteArray(available)
            inputStream.read(bytes, 0, available)
            val text = String(bytes)
        } catch (e: Exception) {
        } finally {
            inputStream.close()
            outputStream.close()
            socket.close()
        }
    }
}