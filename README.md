# MuTTY (MultiPuTTY) - Multi Tab GUI Wrapper around PuTTY for Windows

## Usage

Download PuTTY executable from http://www.putty.org and put it right next to the MuTTY executable - it will auto-detect that.

The **configuration** will be stored in your Users Documents directory. In the MuTTY window, navigate to `Help > About` to find the absolute configuration path.

If you have any SSH Sessions configured in legacy PuTTY, MuTTY will detect them as well, but you will not be able to modify them from MuTTY.

## Development

Simply open the solution in Visual Studio 2019+.

Note that when running in `Debug` configuration, a development configuration file is used (placed in the project directory).

## Acknowledgements

MuTTY uses the DockPanel Suite: http://dockpanelsuite.com/
