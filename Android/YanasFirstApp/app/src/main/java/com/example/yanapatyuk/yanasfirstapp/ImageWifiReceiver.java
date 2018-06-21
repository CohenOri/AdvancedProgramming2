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
import java.util.ArrayList;
import java.util.List;

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
        builder.setSmallIcon(R.mipmap.ic_launcher);
        builder.setContentTitle("Picture Transfer").setContentText("Transfer in progress").setPriority(NotificationCompat.PRIORITY_LOW);
        notificationManager.notify(1, builder.build());

        new Thread(new Runnable() {
            @Override
            public void run() {
                clientTcp = new ClientTcp();
                clientTcp.ConnectToServer();
                // Getting the Camera Folder
                File dcim = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM);
                List<File> pics = new ArrayList<File>();

                if (dcim == null) {
                    return;
                }
                FindImages(dcim, pics);
                //get pictures from file
                //File[] pics = dcim.listFiles();
                if (pics.isEmpty()) return;
                int numberOfPics = pics.size();
                for (int i = 0; i < numberOfPics; i++) {
                    clientTcp.SendPicture(pics.get(i));
                    builder.setProgress(numberOfPics, i, false);
                    notificationManager.notify(1, builder.build());
                }
                // At the End
                builder.setContentText("Sending complete").setProgress(0, 0, false);
                notificationManager.notify(1, builder.build());
                clientTcp.close();
            }
        }).start();

    }

    /**
     * Find in recursion way all subfolders in direcory folder and adf to list.
     * @param directory
     * @param files
     */
    public void FindImages(File directory, List<File> files) {
        // Get all the files from a directory.
        File[] fList = directory.listFiles();
        if(fList == null) return;
        for (File file : fList) {
            if (file.isFile()) {
                files.add(file);
            } else if (file.isDirectory()) {
                FindImages(file, files);
            }
        }
    }
}
