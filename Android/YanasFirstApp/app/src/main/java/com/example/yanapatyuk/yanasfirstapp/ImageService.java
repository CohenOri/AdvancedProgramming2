package com.example.yanapatyuk.yanasfirstapp;

import android.app.Service;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.wifi.WifiManager;
import android.os.IBinder;
import android.widget.Toast;

public class ImageService extends Service {
    private ImageWifiReceiver receiver;
    public ImageService() {
    }

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public void onCreate() {
        super.onCreate();
        // Here put the Code of Service
        this.receiver = new ImageWifiReceiver();
        IntentFilter theFilter = new IntentFilter();
        theFilter.addAction("android.net.wifi.supplicant.CONNECTION_CHANGE");
        theFilter.addAction("android.net.wifi.STATE_CHANGE");
        // Registers the receiver so that your service will listen for broadcasts
        this.registerReceiver(this.receiver, theFilter);
    }

    @Override
    public void onDestroy() {
        Toast.makeText(this,"Service ending...",Toast.LENGTH_SHORT).show();
    }

    @Override
    public int onStartCommand(Intent intent, int flag, int startId) {
        Toast.makeText(this,"Service starting...", Toast.LENGTH_SHORT).show();
        return START_STICKY;
    }




}
