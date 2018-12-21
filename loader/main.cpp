#include "widget.h"
#include "drawwindow.h"
#include <QFileDialog>
#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    Widget *tmp=new Widget();
    QString path=QFileDialog::getOpenFileName(tmp,QObject::tr("Open File"),"",QObject::tr("File (*.off)"));
    delete tmp;
    if(path.length()<5) exit(0);
    QSurfaceFormat format;
    format.setSamples(16);
    TriangleWindow window(path);
    window.setFormat(format);
    window.resize(640, 480);
    window.setAnimating(true);
    window.show();

    return a.exec();
}
