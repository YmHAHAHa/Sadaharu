#include "drawwindow.h"
#include <fstream>
#include<string>



std::vector<MyPoint> allPoint;
std::vector<GLfloat> faceXYZ;
std::vector<GLfloat> faceColor;

TriangleWindow::TriangleWindow(QString path)
    : m_program(0)
    , m_frame(0)
{
    initData(path);
}

void TriangleWindow::initData(QString path)
{
    std::ifstream fs(path.toStdString(),std::ios::in);
    std::string tmp;
    std::getline(fs,tmp);
    if(tmp!="OFF") return;
    std::getline(fs,tmp);
    QStringList slist=QString::fromStdString(tmp).split(' ');
    num_point=slist[0].toInt();
    num_face=slist[1].toInt();
    for(int i=0;i<num_point;++i){
        std::getline(fs,tmp);
        slist=QString::fromStdString(tmp).split(' ');
        allPoint.push_back(MyPoint(slist[0].toFloat(),
                           slist[1].toFloat(),slist[2].toFloat()));
    }

    for(int i=0;i<num_face;++i){
        std::getline(fs,tmp);
        slist=QString::fromStdString(tmp).split(' ');
        ppf=slist[0].toInt();
        for(int j=1;j<=ppf;++j){
            int temp=slist[j].toInt();
            faceXYZ.push_back(allPoint[temp].x);
            faceXYZ.push_back(allPoint[temp].y);
            faceXYZ.push_back(allPoint[temp].z);
            faceColor.push_back(0.0f);
            faceColor.push_back(0.0f);
            faceColor.push_back(1.0f);
        }
    }
}

static const char *vertexShaderSource =
    "attribute highp vec4 posAttr;\n"
    "attribute lowp vec4 colAttr;\n"
    "varying lowp vec4 col;\n"
    "uniform highp mat4 matrix;\n"
    "void main() {\n"
    "   col = colAttr;\n"
    "   gl_Position = matrix * posAttr;\n"
    "}\n";

static const char *fragmentShaderSource =
    "varying lowp vec4 col;\n"
    "void main() {\n"
    "   gl_FragColor = col;\n"
    "}\n";

void TriangleWindow::initialize()
{
    m_program = new QOpenGLShaderProgram(this);
    m_program->addShaderFromSourceCode(QOpenGLShader::Vertex, vertexShaderSource);
    m_program->addShaderFromSourceCode(QOpenGLShader::Fragment, fragmentShaderSource);
    m_program->link();
    m_posAttr = m_program->attributeLocation("posAttr");
    m_colAttr = m_program->attributeLocation("colAttr");
    m_matrixUniform = m_program->uniformLocation("matrix");
}

void TriangleWindow::render()
{
    const qreal retinaScale = devicePixelRatio();
    glViewport(0, 0, width() * retinaScale, height() * retinaScale);

    glClear(GL_COLOR_BUFFER_BIT);

    m_program->bind();

    QMatrix4x4 matrix;
    matrix.perspective(60.0f, 4.0f/3.0f, 0.1f, 100.0f);
    matrix.translate(0, 0, -2);
    matrix.rotate(100.0f * m_frame / screen()->refreshRate(), 0, 1, 0);

    m_program->setUniformValue(m_matrixUniform, matrix);

    /*GLfloat vertices[] = {
        0.0f, 0.8f,0.1f,
        -0.5f, -0.5f,0.2f,
        0.5f, -0.5f,0.2f,
        0.0f, -0.8f,0.1f,
        -0.5f, -0.6f,0.2f,
        0.5f, -0.6f,0.2f
    };

    GLfloat colors[] = {
        1.0f, 0.0f, 0.0f,
        0.0f, 1.0f, 0.0f,
        0.0f, 0.0f, 1.0f,
        1.0f, 0.0f, 0.0f,
        0.0f, 1.0f, 0.0f,
        0.0f, 0.0f, 1.0f
    };*/

    //glVertexAttribPointer(m_posAttr, 3, GL_FLOAT, GL_FALSE, 0, vertices);
    //glVertexAttribPointer(m_colAttr, 3, GL_FLOAT, GL_FALSE, 0, colors);
    glVertexAttribPointer(m_posAttr, 3, GL_FLOAT, GL_FALSE, 0, faceXYZ.data());
    glVertexAttribPointer(m_colAttr, 3, GL_FLOAT, GL_FALSE, 0, faceColor.data());

    glEnableVertexAttribArray(0);
    glEnableVertexAttribArray(1);

    glDrawArrays(GL_TRIANGLES, 0, num_face*3);

    glDisableVertexAttribArray(1);
    glDisableVertexAttribArray(0);

    m_program->release();

    ++m_frame;
}

