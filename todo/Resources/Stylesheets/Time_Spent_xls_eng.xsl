<!--<?xml version="1.0" encoding="UTF-8"?>

    $Id: flat_sorted_todolist.xsl 6917 2008-05-20 08:15:32Z tandler $
    
    Flat task list sorted by priority and due date, 
    Peter Tandler, 2008, http://digital-moderation.com

    parameters for the XSL:
    
        dueuntil=nnnnn - specifies the date until which due tasks 
            should be shown (in MS OLE format as used internally 
            by TDL, days since 30/12/1899)
            if nnnnn = 0, select all tasks
        
        sortby=prio - first sort by prio then by due
        sortby=duedate - first sort by due then by prio
        
        showcomments=1 - show comments, otherwise hide
        
        showflagged=bold    - show flagged tasks in bold
        showflagged=only    - show flagged tasks only, hide others
        
        filename  - the name of the TDL file (used to generate links)
        projectname - the name of the project, per default TODOLIST/@PROJECTNAME otherwise the filename
-->


<!-- copied lots of stuff from ToDoListTableStylesheet_v1.0 by xabatcha@seznam.cz, 2006-10-17 -->
<!-- Feel free to use it or change it. -->
<!-- Transform ToDoList to html, using table layout -->
<!-- Only unfinished tasks are shown, restriction is specified in section 10 -->
<!-- To add other columns to output table please change section 20 and section 32 plus add appropriate part,which load atribute from todolist -->
<!-- ********************************************************** -->
<!-- 2011-03-08, Modifications made by cadraktiv: based on flat_sorted_todolist.xsl by Peter Tandler (see above) -->
<!-- USE WITH CARE - NO WARRANTY. Modified with simple Copy&Paste and Try&Error. -->
<!-- This file is made to generate a simple list of data, extracted from the TDL-files of software "ToDoList"-->
<!-- This file displays 
        ALL         done and not-done tasks
        WHICH       have a "time spent"
        AND WITH    "Time / Done (yes/no) / Allocated to / Task name"    -->
<!-- For more configuration with the "params" see describtion above. -->
<!-- There is a lot of unused and copy-paste-trash-designed code inside. You are invited to clean it up.-->
<!-- ********************************************************** -->
<!-- ********************************************************** -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:strip-space elements="*"/>
    <xsl:output method="text"/>

    <xsl:param name="sortby" select="'prio'" />
    <!--<xsl:param name="sortby" select="'TIMESPENT'" />-->
    <xsl:param name="dueuntil" select="0" />
    <xsl:param name="showcomments" select="0" />
    <xsl:param name="showflagged" select="'bold'" />
    
    <xsl:param name="filename" select="TODOLIST/@FILENAME"/>
    <xsl:param name="projectname">
        <xsl:choose>
            <xsl:when test="string-length(TODOLIST/@PROJECTNAME) &gt; 0">
                <xsl:value-of select="TODOLIST/@PROJECTNAME"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$filename"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:param>
    
    
        
    <xsl:template match="TODOLIST">
            <xsl:text>Project Report 'Spent-Times': </xsl:text> 
                        <xsl:value-of select="@PROJECTNAME"/>
                        <xsl:text>&#10;</xsl:text>
            <xsl:text>Date of Report: </xsl:text>
                        <xsl:value-of select="@REPORTDATE" />
                        <xsl:text>&#10;</xsl:text>
            <xsl:text>Fileversion </xsl:text>
                        <xsl:value-of select="@FILEVERSION" /> from <xsl:value-of select="@LASTMODIFIED" /> 
                        <xsl:text> (sorry, only current date)&#10;&#10;</xsl:text>
        
            <xsl:text>Arbeitszeit&#9;Erledigt&#9;Zugewiesen an&#9;Aufgabe&#10;</xsl:text>
        <xsl:choose>
            <xsl:when test="$sortby='prio'">
                <!-- first take all tasks with a due date -->
                <xsl:for-each select="//TASK[($dueuntil = 0 or @DUEDATE &lt; $dueuntil) and ($showflagged != 'only' or @FLAG) and (@TIMESPENT)]">
                    <xsl:sort 
                        select="@PRIORITY" 
                        data-type="number"
                        order="descending"
                        />
                    <xsl:sort 
                        select="@STARTDATE" 
                        data-type="number"
                        />
                    <xsl:sort 
                        select="@DUEDATE" 
                        data-type="number"
                        />
                    <xsl:sort 
                        select="@ID" 
                        data-type="number"
                        />
                    
                    <!--<xsl:if test="not(@DONEDATESTRING)"> -->
                        <xsl:call-template name="get_Task"/>
                    <!--</xsl:if> -->
                </xsl:for-each>
            </xsl:when>
            <xsl:otherwise>
                <!-- sort first by due date then by prio -->
                <xsl:for-each select="//TASK[@DUEDATE and ($dueuntil = 0 or @DUEDATE &lt; $dueuntil) and ($showflagged != 'only' or @FLAG) and (@TIMESPENT)]">
                    <xsl:sort 
                        select="@TIMESPENT" 
                        data-type="number"
                        />
                    <xsl:sort 
                        select="@STARTDATE" 
                        data-type="number"
                        />
                    <xsl:sort 
                        select="@DUEDATE" 
                        data-type="number"
                        />
                    <xsl:sort 
                        select="@PRIORITY" 
                        data-type="number"
                        order="descending"
                        />
                    <xsl:sort 
                        select="@ID" 
                        data-type="number"
                        />
                    
                    <!--<xsl:if test="not(@DONEDATESTRING)"> -->
                        <xsl:call-template name="get_Task"/>
                    <!--</xsl:if> -->
                </xsl:for-each>
                
                <!-- then take all tasks without a due date -->
                <xsl:if test="$dueuntil = 0">
                <xsl:for-each select="//TASK[not(@DUEDATE) and ($showflagged != 'only' or @FLAG) and (@TIMESPENT)]">
                    <xsl:sort 
                        select="@PRIORITY" 
                        data-type="number"
                        order="descending"
                        />
                    <xsl:sort 
                        select="@ID" 
                        data-type="number"
                        />
                    
                    <xsl:if test="not(@DONEDATESTRING)">
                        <xsl:call-template name="get_Task"/>
                    </xsl:if>
                </xsl:for-each>
                </xsl:if>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>


    <!-- 20 - header of table - titles of columns -->
    <xsl:template name="get_Header">
        <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
<!--
            <xsl:element name="td">
                <xsl:attribute name="class">header headerid</xsl:attribute>
                <xsl:attribute name="width">15</xsl:attribute>
                <xsl:text>ID</xsl:text>
            </xsl:element>
            <xsl:element name="td">
                <xsl:attribute name="class">header headerprior</xsl:attribute>
                <xsl:attribute name="width">15</xsl:attribute>
                <xsl:text>!</xsl:text>
            </xsl:element>
            <xsl:element name="td">
                <xsl:attribute name="class">header headerprogress</xsl:attribute>
                <xsl:attribute name="width">25</xsl:attribute>
                <xsl:text>%</xsl:text>
            </xsl:element>
-->
            <xsl:element name="td">
                <xsl:attribute name="class">header</xsl:attribute>
                <xsl:attribute name="width">100</xsl:attribute>
                <xsl:text>Arbeitszeit</xsl:text>
            </xsl:element>
            <xsl:element name="td">
                <xsl:attribute name="class">header</xsl:attribute>
                <xsl:attribute name="width">30</xsl:attribute>
                <xsl:text>Erledigt</xsl:text>
            </xsl:element>
            <xsl:element name="td">
                <xsl:attribute name="class">header</xsl:attribute>
                <xsl:attribute name="width">120</xsl:attribute>
                <xsl:text>Zugewiesen an</xsl:text>
            </xsl:element>
<!--
            <xsl:element name="td">
                <xsl:attribute name="class">header</xsl:attribute>
                <xsl:attribute name="width">50</xsl:attribute>
                <xsl:text>Category</xsl:text>
            </xsl:element>
            <xsl:element name="td">
                <xsl:attribute name="class">header</xsl:attribute>
                <xsl:attribute name="width">50</xsl:attribute>
                <xsl:text>Status</xsl:text>
            </xsl:element>
-->
            <xsl:element name="td">
                <xsl:attribute name="class">header</xsl:attribute>
                <xsl:text>Aufgabe</xsl:text>
            </xsl:element>
        </xsl:element>
    </xsl:template>


    <!-- 31- single task -->
    <xsl:template name="get_Task">
            <xsl:call-template name="get_Task_Details"/>
    </xsl:template>




    <!--32 - detail of single task -->  
    <xsl:template name="get_Task_Details">
<!--
        <xsl:call-template name="get_ID"/>
        <xsl:call-template name="get_Priority"/>
        <xsl:call-template name="get_Progress"/>
        <xsl:call-template name="get_due"/>
        -->
        <xsl:call-template name="get_spent"/>
        <xsl:call-template name="get_done"/>
        <xsl:call-template name="get_to"/>
<!--
        <xsl:call-template name="get_Category"/>
        <xsl:call-template name="get_Status"/>
        -->
        <xsl:call-template name="get_Task_title"/>
        <xsl:text>&#10;</xsl:text>
    </xsl:template>
    
    <!-- 40 - @PERCENTDONE as progress -->  
    <xsl:template name="get_Progress">
        <xsl:element name="td">
            <xsl:attribute name="class">progress bbasic</xsl:attribute>
            <xsl:value-of select="@PERCENTDONE"/>
        </xsl:element>
    </xsl:template>
    
    <!-- 41 - @DUEDATESTRING if exists -->
    <xsl:template name="get_due">
        <xsl:element name="td">
            <xsl:choose>
                <xsl:when test="(@DUEDATESTRING)">
                    <xsl:attribute name="class">due bbasic</xsl:attribute>
                    <xsl:value-of select="@DUEDATESTRING"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:attribute name="class">due bbasic empty</xsl:attribute>
                    <xsl:text>.</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:element>
    </xsl:template>
    
    <!-- 41b - @STARTDATESTRING if exists -->
    <xsl:template name="get_start">
        <xsl:element name="td">
            <xsl:choose>
                <xsl:when test="(@STARTDATESTRING)">
                    <xsl:attribute name="class">start bbasic</xsl:attribute>
                    <xsl:value-of select="@STARTDATESTRING"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:attribute name="class">start bbasic empty</xsl:attribute>
                    <xsl:text>.</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:element>
    </xsl:template>
    
    <!-- 41c - @TIMESPENT if exists -->
    <xsl:template name="get_spent">
            <xsl:choose>
                <xsl:when test="(@TIMESPENT)">
                    <xsl:value-of select='format-number((@TIMESPENT), "#.00")'/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>"---"</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>&#9;</xsl:text>
    </xsl:template>
    
    <!-- 41c - Done or not-->
    <xsl:template name="get_done">
            <xsl:choose>
                <xsl:when test="(@DONEDATESTRING)">
                    <xsl:text>X</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>-</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>&#9;</xsl:text>
    </xsl:template>
    
    <!-- 42 - @PRIORITY as colored priority -->
    <xsl:template name="get_Priority">
        <xsl:choose>
            <xsl:when test="@PRIORITY &lt; 0">
                <xsl:element name="td">
                    <xsl:attribute name="class">prior bbasic empty</xsl:attribute>
                    <xsl:text>.</xsl:text>
                </xsl:element>
            </xsl:when>
            <xsl:otherwise>
                <xsl:element name="td">
                    <xsl:attribute name="class">prior bbasic</xsl:attribute>
                    <xsl:attribute name="style">background-color: <xsl:value-of select="@PRIORITYWEBCOLOR"/>;</xsl:attribute>
                    <xsl:value-of select="@PRIORITY"/>
                </xsl:element>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <!-- 43 - @PERSON as person who should do a work -->    
<xsl:template name="get_to">
			<xsl:choose>
				<xsl:when test="(@PERSON)">
					<xsl:value-of select="@PERSON"/>
					<!-- actually it would be good to check NUMPERSON and have a loop here (2011-02-25-pt) -->
					<xsl:if test="@PERSON1">; <xsl:value-of select="@PERSON1" />
					</xsl:if>
					<xsl:if test="@PERSON2">; <xsl:value-of select="@PERSON2" />
					</xsl:if>
					<xsl:if test="@PERSON3">; <xsl:value-of select="@PERSON3" />
					</xsl:if>
					<xsl:if test="@PERSON4">; <xsl:value-of select="@PERSON4" />
					</xsl:if>
					<xsl:if test="@PERSON5">; <xsl:value-of select="@PERSON5" />
					</xsl:if>
					<xsl:if test="@PERSON6">; <xsl:value-of select="@PERSON6" />
					</xsl:if>
					<xsl:if test="@PERSON7">; <xsl:value-of select="@PERSON7" />
					</xsl:if>
					<xsl:if test="@PERSON8">; <xsl:value-of select="@PERSON8" />
					</xsl:if>
					<xsl:if test="@PERSON9">; <xsl:value-of select="@PERSON9" />
					</xsl:if>
					
				</xsl:when>
				<xsl:otherwise>
					<xsl:text>.</xsl:text>
				</xsl:otherwise>
			</xsl:choose>
            <xsl:text>&#9;</xsl:text>
	</xsl:template>
    
    <!-- 44 - @CATEGORY if exists -->   
    <xsl:template name="get_Category">
        <xsl:element name="td">
            <xsl:choose>
                <xsl:when test="(@CATEGORY)">
                    <xsl:attribute name="class">category bbasic</xsl:attribute>
                    <xsl:value-of select="@CATEGORY"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:attribute name="class">category bbasic empty</xsl:attribute>
                    <xsl:text>.</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:element>
    </xsl:template>

    <!-- 44b - @STATUS if exists -->    
    <xsl:template name="get_Status">
        <xsl:element name="td">
            <xsl:choose>
                <xsl:when test="(@STATUS)">
                    <xsl:attribute name="class">status bbasic</xsl:attribute>
                    <xsl:value-of select="@STATUS"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:attribute name="class">status bbasic empty</xsl:attribute>
                    <xsl:text>.</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:element>
    </xsl:template>

    <!-- 45 - @TITLE as title +  @COMMENTS if exists-->
    <xsl:template name="get_Task_title">
            <!-- <xsl:call-template name="tab"/> -->
                <xsl:value-of select="@TITLE"/>
    </xsl:template>
    
    <!-- 46 - @ID -->
    <xsl:template name="get_ID">
        <xsl:element name="td">
            <xsl:attribute name="class">bbasic taskid</xsl:attribute>
            <xsl:value-of select="@ID"/>
        </xsl:element>
    </xsl:template>
    
    
    
    <!-- 50 - It puts a space in -->
    <xsl:template name="tab">
        <xsl:if test="count(ancestor::TASK)>0">
            <xsl:for-each select="(ancestor::TASK)">
                <xsl:element name="span">
                    <xsl:attribute name="class">tab</xsl:attribute>
                </xsl:element>
            </xsl:for-each>
        </xsl:if>
    </xsl:template>
    
    
    <!-- 51 - Write Task's Ancesor Path in parenteses -->
    <xsl:template name="get_Task_Ancestors">
        <xsl:if test="count(ancestor::TASK)>0">
            <xsl:text> ( </xsl:text>
            <xsl:for-each select="(ancestor::TASK)">
                <xsl:value-of select="@TITLE"/>
                <xsl:text> - </xsl:text>
            </xsl:for-each>
            <xsl:text>)</xsl:text>
        </xsl:if>
    </xsl:template>
    
    
    <!-- 60 - retain CRLF within comments by adding BR elements ()-->
    <xsl:template name="fix-breaks">
        <xsl:param name="text" />
        <xsl:choose>
            <xsl:when test="contains($text,'&#13;&#10;')">
                <xsl:value-of select="substring-before($text,'&#13;&#10;')" />
                <xsl:element name="br"/>
                <xsl:call-template name="fix-breaks">
                    <xsl:with-param name="text">
                        <xsl:value-of select="substring-after($text,'&#13;&#10;')" />
                    </xsl:with-param>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$text" />
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    

</xsl:stylesheet>
