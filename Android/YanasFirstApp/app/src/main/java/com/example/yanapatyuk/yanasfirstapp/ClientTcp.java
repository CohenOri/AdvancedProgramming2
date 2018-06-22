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
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

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
            this.serverAddr = InetAddress.getByName("192.168.31.86");
            //create a socket to make the connection with the server
            this.socket = new Socket(serverAddr, 8005);
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
            /*OutputStream output = socket.getOutputStream();
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
                output.flush();*/

            //InputStream inputStream = socket.getInputStream();
            //Log.e("TCP", "YES!");
            //FileInputStream fis = new FileInputStream(pic);
            //Bitmap bm = BitmapFactory.decodeStream(fis);
            //byte[] imgbyte = getBytesFromBitmap(bm);
            //byte[] b = new byte[1];

            //Socket socket = new Socket(serverAddr, 8005);
            OutputStream outputStream = socket.getOutputStream();
            InputStream inputStream = socket.getInputStream();
            FileInputStream fis = new FileInputStream(pic);
            Bitmap bm = BitmapFactory.decodeStream(fis);
            byte[] imgbyte = getBytesFromBitmap(bm);
            byte[] b = new byte[1];
            outputStream.write(pic.getName().getBytes());


            inputStream.read(b,0,1); // wait for finished reading img byte

            // convert length to byte array
            ByteBuffer buffer = ByteBuffer.allocate(8);
            buffer.order(ByteOrder.LITTLE_ENDIAN);
            buffer.putLong(imgbyte.length);
            outputStream.write(buffer.array(),0,8); // length

            if (inputStream.read(b,0,1) == 1) { // wait for finish reading length bytes
                outputStream.write(imgbyte);
            }
            inputStream.read(b,0,1); // wait for finished reading img byte
            outputStream.flush();

            /*
            outputStream.write(longToBytes(pic.length())); // write pic size length
            outputStream.write(longToBytes(pic.getName().length())); // write pic name length

            // read pic, and convert to byte[]
            byte[] img = new byte[(int) pic.length()]; //init array with file length
            FileInputStream f = new FileInputStream(pic);
            f.read(img); //read file into bytes[]
            f.close();

            outputStream.write(img); // write actual pic
            outputStream.write(pic.getName().getBytes()); // write pic name

            outputStream.flush();

            /*bar = bar + 100 / pics.length;
            builder.setProgress(100, bar, false);
            notificationManager.notify(10, builder.build());*/

            //socket.close();*/

        } catch (Exception e) {
            Log.e("TCP", "S: Error", e);
        }
    }


    /*public byte[] longToBytes(long x) {
        //ByteBuffer buffer = ByteBuffer.allocate(Long.BYTES);
        ByteBuffer buffer = ByteBuffer.allocate(8);
        buffer.putLong(x);
        return buffer.array();
    }*/

    /**
     * close the socket.
     */
    public void close() {
        try {
            OutputStream output = socket.getOutputStream();
            //output.write(-1);
            output.flush();
            output.close();
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
