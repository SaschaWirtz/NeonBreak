package de.MGD.NeonBreak.transports.bluetoothTransport;

import java.util.UUID;
import java.lang.Thread;
import java.io.IOException;
import android.bluetooth.BluetoothServerSocket;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothSocket;
import androidx.appcompat.app.AppCompatActivity;


class BTServerController(activity: AppCompatActivity) : Thread() {
    private var cancelled: Boolean
    private val serverSocket: BluetoothServerSocket?
    private val activity = activity

    companion object {
        val uuid: UUID = UUID.fromString("b8f94722-384b-4c3a-9e6e-ec66dc725f1a")
    }

    init {
        val btAdapter = BluetoothAdapter.getDefaultAdapter()
        if (btAdapter != null) {
            this.serverSocket = btAdapter.listenUsingRfcommWithServiceRecord("NeonBreak", uuid)
            this.cancelled = false
        } else {
            this.serverSocket = null
            this.cancelled = true
        }

    }

    override fun run() {
        var socket: BluetoothSocket

        while(true) {
            if (this.cancelled) {
                break
            }

            try {
                socket = serverSocket!!.accept()
            } catch(e: IOException) {
                break
            }

            if (!this.cancelled && socket != null) {
                BTServer(this.activity, socket).start()
            }
        }
    }

    fun cancel() {
        this.cancelled = true
        this.serverSocket!!.close()
    }
}