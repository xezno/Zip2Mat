# Zip2Mat

![Zip2Mat Screenshot](https://user-images.githubusercontent.com/12881812/115127174-6a453f80-9fcc-11eb-8da5-7c99f23d184d.png)

## What?

Zip2Mat is a simple app that allows you to convert textures contained in zip files 
(like [this one](https://cc0textures.com/view?id=Metal022)) to materials you can use with Source 2, allowing you to import
materials at light speed.

Zip2Mat currently features:

- A super simple, intuitive interface
- Support for the 'VR Simple' shader
- Tested & works with CC0Textures, TextureCan, TextureHaven, among others (most of the [3dassets.one](https://3dassets.one/)
results should work without issue)
- Automatically extracts & moves all of your files into the right place so that you don't have to

In the near future, Zip2Mat will allow you to:

- Convert textures into .tga images (Source 2 likes these more than jpegs or pngs or whatever)
- Normalize file names (so that your project folder isnt a mess)
- Drag & drop multiple zip files so that you can batch import textures

## Why?

Because I'm lazy and can't be bothered to link all of the maps up manually. And you probably don't either.

## Where?

You can [download the latest release](https://github.com/xezno/Zip2Mat/releases) with this repo's contents and compile it
yourself, or [download the zip](https://github.com/xezno/Zip2Mat/archive/refs/heads/main.zip).

**As a quick note**, don't bother using jpegs. Importing them into Source 2 causes the whole engine to hang for ~20 seconds,
and they look like shit anyway.

## How?

Zip2Mat simply searches for common aliases for the different map types (i.e. color -> colour, diffuse, col, albedo, etc.)
and then generates a vmat file using the T4 text template system.