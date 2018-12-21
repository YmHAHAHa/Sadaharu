#ifndef DRAWWINDOW_H
#define DRAWWINDOW_H

#include "openglwindow.h"
#include <QtGui/QGuiApplication>
#include <QtGui/QMatrix4x4>
#include <QtGui/QOpenGLShaderProgram>
#include <QtGui/QScreen>
#include <QtCore/qmath.h>
#include <vector>

struct MyPoint
{
    GLfloat x,y,z;
    MyPoint(GLfloat a,GLfloat b,GLfloat c){x=a;y=b;z=c;}
};

class TriangleWindow : public OpenGLWindow
{
public:
    TriangleWindow(QString path);

    void initialize() override;
    void render() override;

private:
    GLuint m_posAttr;
    GLuint m_colAttr;
    GLuint m_matrixUniform;

    QOpenGLShaderProgram *m_program;
    int m_frame;

    int num_point;
    int num_face;
    int ppf;

    void initData(QString path);
};

#endif // DRAWWINDOW_H
