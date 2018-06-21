package com.example.yanapatyuk.yanasfirstapp;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Environment;
import android.support.v4.app.NotificationCompat;
import android.support.v4.app.NotificationManagerCompat;
import android.util.Log;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;

public class ClientTcp  {
    private InetAddress serverAddr;
    private  Socket socket;
    public ClientTcp() {}

    /**
     * Connect to the server
     */
    public void ConnectToServer() {
        try {
            //here you must put your computer's IP address.
            this.serverAddr = InetAddress.getByName("192.168.1.11");
            //create a socket to make the connection with the server
            this.socket = new Socket(serverAddr, 65534);
        } catch (Exception e) {
            Log.e("TCP", "C: Error", e);
            this.serverAddr = null;
            this.socket = null;
        }
    }

    /**
     * send picture to server
     * @param pic file
     */
    public void SendPicture(File pic) {
        try {
            //sends the message to the server
            OutputStream output = socket.getOutputStream();
                FileInputStream fis = new FileInputStream(pic);
                Bitmap bm = BitmapFactory.decodeStream(fis);
                byte[] imgbyte = getBytesFromBitmap(bm);
                byte[] picName = pic.getName().getBytes();
                int sizeOfPic = imgbyte.length;
                int sizeOfName = picName.length;
                output.write(sizeOfPic);
                output.write(sizeOfName);
                output.write(imgbyte);
                output.write(picName);
                output.flush();
        } catch (Exception e) {
            Log.e("TCP", "S: Error", e);
        }
    }

    /**
     * close the socket.
     */
    public void close() {
        try {
            OutputStream output = socket.getOutputStream();
            output.write(-1);
            output.flush();
            this.socket.close();
        } catch (IOException e) {
            Log.e("TCP", "C: Error", e);
        }
    }

    /**
     * convart picture to image
     * @param bitmap
     * @return
     */
    public byte[] getBytesFromBitmap(Bitmap bitmap) {
        ByteArrayOutputStream stream = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.PNG, 70, stream);
        return stream.toByteArray();
    }

}
