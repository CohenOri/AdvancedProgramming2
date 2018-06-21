package com.example.yanapatyuk.yanasfirstapp;

import android.content.Intent;
import android.os.Environment;
import android.support.v4.app.NotificationCompat;
import android.support.v4.app.NotificationManagerCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import java.io.File;


public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    /**
     * Connect the service
     * @param view
     */
    public void ConnectToService(View view) {
        Intent intent = new Intent(this, ImageService.class);
        startService(intent);
    }

    /**
     *
     * @param view
     */
    public void Disconnect(View view) {
        Intent intent = new Intent(this, ImageService.class);
        stopService(intent);
    }

}
