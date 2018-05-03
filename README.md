# Image Occlusion Editor

***Image Occlusion Editor*** is companion software to *SuperMemo*. It is meant as an **alternative** to the **built-in Image Occlusion** template, with the advantage of supporting multiple screen resolutions.

This software works in tandem with SuperMemo Assistant, found [on this same GitHub profile](https://github.com/supermemo/SuperMemoAssistant)

[**All releases**](https://github.com/supermemo/ImageOcclusionEditor/releases)

[**Latest version (installer)**](https://github.com/supermemo/ImageOcclusionEditor/releases/download/1.0/ImageOcclusionEditor_v1.0_Setup.msi)

[![Image Occlusion Editor](https://raw.githubusercontent.com/SuperMemo/ImageOcclusionEditor/master/ImageOcclusionEditor-v1.0.png)](https://raw.githubusercontent.com/SuperMemo/ImageOcclusionEditor/master/ImageOcclusionEditor-v1.0.png)

## Information

**Usage**: `ImageOcclusionEditor.exe <BackgroundFile> <OcclusionFile>`

**Editing**: The SVG code is embedded inside the PNG Occlusion file, enabling edition of existing occlusions. Simply open them again with Image Occlusion Editing like you normally would.


## Demonstration video
[![Image Occlusion Editor Demonstration](https://img.youtube.com/vi/BJ1ZAYSGJ4M/0.jpg)](https://youtu.be/BJ1ZAYSGJ4M)


## Configuration (Optional)

In **App.config**:
```
<setting name="StrokeColor" serializeAs="String">
  <value>2D2D2D</value>
</setting>
<setting name="StrokeWidth" serializeAs="String">
  <value>2</value>
</setting>
<setting name="FillColor" serializeAs="String">
  <value>FFEBA2</value>
</setting>
```

* **StrokeColor**: Default occlusion border color
* **StrokeWidth**: Default occlusion border width
* **FillColor**: Default occlusion background color

## Special thanks, Credits, Licenses

*Image Occlusion Editor* is built on the work of people who pledged their time to the Open Source community.

I would like to emphasize on the importance of their contribution, and extend my gratitude especially to (but not limited to):
* The people of the *SVG-Edit* group for their [SVG editor](https://github.com/SVG-Edit/svgedit) (central piece of ImageOcclusionEditor)
* The people of the *vvvv* group for their [SVG library](https://github.com/vvvv/SVG)
* *Neil Harvey* for his [FileSignature library](https://github.com/neilharvey/FileSignatures)
* *Aristotelis P.* and predecessors for [the original idea](https://github.com/glutanimate/image-occlusion-enhanced), and years of using their Occlusion Addon in the past

All required licenses can be found at the root of this project repository.
If however you found that I omitted to include the terms of the license for one of your work, by all mean please let me know so that I may correct this.
