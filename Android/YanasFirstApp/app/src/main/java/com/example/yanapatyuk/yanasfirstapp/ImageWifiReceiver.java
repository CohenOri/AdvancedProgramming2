package com.example.yanapatyuk.yanasfirstapp;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiManager;
import android.os.Environment;
import android.support.v4.app.NotificationCompat;
import android.support.v4.app.NotificationManagerCompat;

import java.io.File;

public class ImageWifiReceiver extends BroadcastReceiver {
private ClientTcp clientTcp;
    @Override
    public void onReceive(Context context, Intent intent) {
        // TODO: This method is called when the BroadcastReceiver is receiving
        // an Intent broadcast.
        WifiManager wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);

        NetworkInfo networkInfo = intent.getParcelableExtra(WifiManager.EXTRA_NETWORK_INFO);
        if (networkInfo != null) {
            if (networkInfo.getType() == ConnectivityManager.TYPE_WIFI) {
                //get the different network states
                if (networkInfo.getState() == NetworkInfo.State.CONNECTED) {
                    startTransfer(context);
                }
            }
        }

    }

    /**
     * Connect to the server and send the pictures
     */
    private void startTransfer(Context context) {
       final NotificationManagerCompat notificationManager = NotificationManagerCompat.from(context);
       final NotificationCompat.Builder builder = new NotificationCompat.Builder(context, "default");
        builder.setContentTitle("Picture Transfer").setContentText("Transfer in progress").setPriority(NotificationCompat.PRIORITY_LOW);
        new Thread(new Runnable() {
            @Override
            public void run() {
                clientTcp = new ClientTcp();
                clientTcp.ConnectToServer();
                // Getting the Camera Folder
                File dcim = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM);
                if (dcim == null) {
                    return;
                }
                //get pictures from file
                File[] pics = dcim.listFiles();
                if(pics == null) return;
                int numberOfPics = pics.length;
                for(int i = 0; i < numberOfPics; i++) {
                    clientTcp.SendPicture(pics[i]);
                    builder.setProgress(numberOfPics, i,false);
                    notificationManager.notify(1, builder.build());

                }
                // At the End
                builder.setContentText("Sending complete").setProgress(0, 0, false);
                notificationManager.notify(1, builder.build());
                clientTcp.close();
            }
        }).start();

    }

    }
