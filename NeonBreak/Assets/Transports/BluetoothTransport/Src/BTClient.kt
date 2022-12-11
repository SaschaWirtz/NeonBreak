package de.MGD.NeonBreak.transports.bluetoothTransport;

import java.lang.Thread;
import java.lang.Exception;
import android.bluetooth.BluetoothDevice

// class BTClient(device: BluetoothDevice): Thread() {
//     private val socket = device.createRfcommSocketToServiceRecord(BTServerController.uuid)

//     override fun run() {
//         this.socket.connect()

//         val outputStream = this.socket.outputStream
//         val inputStream = this.socket.inputStream
//         try {
//             outputStream.write(message.toByteArray())
//             outputStream.flush()
//         } catch(e: Exception) {
//         } finally {
//             outputStream.close()
//             inputStream.close()
//             this.socket.close()
//         }
//     }
// }