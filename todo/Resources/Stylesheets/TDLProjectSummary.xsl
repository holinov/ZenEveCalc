<?xml version="1.0" ?>
<!-- 
**************** TDL to Project summary **************** 

*******************************************
V1.1 - Author BOSC Laurent 20090910
***Inspired from :
V2.0 ToDoListStyler Original by Hoppfrosch@gmx.de, 20061105
V1.0 ToDoListStyler Original by Manual Reyes 
*******************************************
CHANGE - LOG:
|!!| Important
|+| New
|-|Fixed error
|*| Changes
|?| Open questions

- Previous versions :
1.1 - Author BOSC Laurent 20090910
	 |+| Add Field 'Risk'
1.0 - Author BOSC Laurent 20090617
	* |!!| Works only with TDL Version newer than 5.7
	* |*| tree or list view(depend on which tab you are in TDL) of tasks, with  specific style for the 2 first levels (upcase, underline, specific color)
	* |*| Fields (on a single line) 'title' (Colored)  - 'Start Date' - 'Due Date' - 'Allocated To'(multiple) - Allocated By' - 'Priority'(colored) - Status - Category - 'ID' - 'Next IDs' (multiple)  - '%Complete' (with progress bar)  - File Reference - comments
*******************************************
-->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:strip-space elements="*" />
	
	<xsl:template match="TODOLIST">
		<xsl:element name="html">
			<xsl:element name="head">
				<xsl:element name="title">
					<xsl:value-of select="@PROJECTNAME" />
				</xsl:element>
				<xsl:element name="style">
					body
					{
						font-family: Tahoma, serif;
						margin: 5px 5px 5px 5px;
						background-color : #C0C0C0;
						font-size: 11px;
					}
					
					h5
					{
						font-size: 8px;
					}
					
					.headerSpan
					{
						background-color: #C4D7FF;
						border-left: 1px solid #006699;
						border-right: 1px solid #006699;
						border-bottom: 1px solid #006699;
						border-top: 1px solid #006699;
						padding-top: 2px; 
						padding-bottom: 2px;
						font-size: 11px;
						text-align: center;
						width :100%;
					}
					
					.footerSpan
					{
						background-color: #C4D7FF;
						border-left: 1px solid #006699;
						border-right: 1px solid #006699;
						border-bottom: 1px solid #006699;
						border-top: 1px solid #006699;
						padding-top: 2px; 
						padding-bottom: 2px;
						font-size: 11px;
						text-align: center;
						width :100%;
					}
					
					.masterTaskSpan
					{
						background-color: #FFFFFF;
						border-left: 1px solid #006699;
						border-right: 1px solid #006699;
						border-bottom: 1px solid #006699;
						border-top: 1px solid #006699;
						padding-top: 2px; 
						padding-bottom: 2px;
						padding-left: 2px; 
						padding-right: 2px;
						font-size: 11px;
						text-align: left;
						width :100%;
					}
					
					.completedMasterTaskSpan
					{
						background-color: #778899;
						border-left: 1px solid #006699;
						border-right: 1px solid #006699;
						border-bottom: 1px solid #006699;
						border-top: 1px solid #006699;
						padding-top: 2px; 
						padding-bottom: 2px;
						padding-left: 2px; 
						padding-right: 2px;
						font-size: 11px;
						text-align: left;
						width :100%;
					}
					
					.progressBarBorder 
					{
			            height: 5px;
			            width: 100px;
			            background: #fff;
			            border: 1px solid silver;
			            margin: 0;
			            padding: 0;
			          }

					.progressBar {
			            height: 2px;
			            margin: 1px;
			            padding: 0;
			            background: #C9DDEC;
			          }
					
					.prettyPriority
					{
						width:20px;
						text-align:center;
						color: black;
					}
										
					.masterCompletedTaskTitleText
					{
						font-weight: bold;
						font-size: 14px;
						color: blue;						
						text-align: left;
						text-transform: uppercase;						
						text-decoration: line-through;						
					}
					
					.masterTaskTitleText
					{
						font-weight: bold;
						font-size: 14px;
						color: blue;						
						text-align: left;
						text-transform: uppercase;
						text-decoration: underline;
					}
					
					.masterCompletedTaskTitleText2
					{
						font-weight: bold;
						text-align: left;
						text-transform: uppercase;						
						text-decoration: line-through;						
					}
					
					.masterTaskTitleText2
					{
						font-weight: bold;
						text-align: left;
						text-transform: uppercase;
						text-decoration: underline;
					}
					
					.subCompletedTaskTitleText
					{
						font-weight: bold;
						text-decoration: line-through;
					}
										
					.subTaskTitleText
					{
						font-weight: bold;
					}
									
					.completedBaseInfoText
					{
						color: #C0C0C0;
					}
					
					.baseInfoText
					{
						color: #000000;
					}
					
					.completedDatesText
					{
						color: #C0C0C0;
					}
					
					.datesText
					{
						color: #000000;
					}
					
					.completedPeopleText
					{
						color: #C0C0C0;
					}
					
					.peopleText
					{
						color: #000000;
					}
					
					.completedFilerefpathText
					{
						color: #C0C0C0;
					}
					
					.filerefpathText
					{
						color: #000000;
					}
					
					.completedCommentsText
					{
						color: #C0C0C0;
					}
					
					.commentsText
					{
						font-style: italic;						
						color: #000000;
					}
					
					.priorityNumberStyle
					{
						font-weight: bold;
						color: black;
					}
					
					.riskNumberStyle
					{
						font-weight: bold;
						color: black;
					}
				</xsl:element>
			</xsl:element>
			<xsl:element name="body">
				<!-- HEADER SPAN -->
				<xsl:element name="div">
					<xsl:attribute name="class">headerSpan</xsl:attribute>
					<xsl:element name="h2">Project Summary</xsl:element>
					<xsl:element name="h3"><xsl:value-of select="@PROJECTNAME"/></xsl:element>
				</xsl:element>
				
				<!-- SPACER -->
				<xsl:element name="br"/>
				<xsl:element name="br"/>
				
				<!-- TASK INFORMATION -->
				<xsl:apply-templates select ="TASK"/>
				
				<!-- FOOTER SPAN -->
				<xsl:element name="div">
					<xsl:attribute name="class">footerSpan</xsl:attribute>
					<xsl:element name="h5">Generated from TodoList - http://www.abstractspoon.com<xsl:element name="br"/>Using TDLProjectSummary.xsl v1.0</xsl:element>
				</xsl:element>
			</xsl:element>
		</xsl:element>
	</xsl:template>	
	<xsl:template match="TASK">
		<xsl:choose>
			<xsl:when test="TASK">
				<xsl:choose>
					<xsl:when test="parent::TODOLIST">
						<!-- first task level  -->
						<xsl:element name="div">
							<xsl:attribute name="class">masterTaskSpan</xsl:attribute>
							<xsl:choose>								
								<xsl:when test="child::TASK">
									<xsl:call-template name="get_MasterTask_Details" />
									<xsl:element name="ul">
										<xsl:apply-templates select ="TASK">
											<xsl:sort select="@POS"/>
										</xsl:apply-templates>
									</xsl:element>
								</xsl:when>
								<xsl:otherwise>
									<!-- sub task at first level  -->
									<xsl:call-template name="get_Task_Details" />
								</xsl:otherwise>
							</xsl:choose>							
						</xsl:element>
						<!--<xsl:element name="br"/>-->
						<xsl:element name="br"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>							
							<xsl:when test="child::TASK">
								<xsl:if test="../../../TODOLIST">
									<xsl:element name="br"/>									
								</xsl:if>
								<xsl:call-template name="get_MasterTask_Details" />								
								<xsl:element name="ul">
									<xsl:apply-templates select ="TASK"/>
								</xsl:element>
							</xsl:when>							
							<xsl:otherwise>
								<!-- sub task  -->
								<xsl:call-template name="get_Task_Details" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="parent::TODOLIST">
						<!-- first task level  -->
						<xsl:element name="div">
							<xsl:attribute name="class">masterTaskSpan</xsl:attribute>							
							<!-- sub task at first level  -->
							<xsl:call-template name="get_Task_Details" />					
						</xsl:element>						
						<!--<xsl:element name="br"/>-->
						<xsl:element name="br"/>
					</xsl:when>
					<xsl:otherwise>
						<!-- sub task  -->
						<xsl:call-template name="get_Task_Details" />					
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--
	Retrieves task details
	-->
	<xsl:template name="get_MasterTask_Details">
		<li>
			<xsl:call-template name="get_MasterTask_Title" />
		</li>
	</xsl:template>	
	<xsl:template name="get_Task_Details">
		<li>
			<xsl:call-template name="get_Task_Title" />
			<xsl:text>   </xsl:text> 
			<xsl:call-template name="get_Task_Dates" />
			<xsl:text>   </xsl:text>
			<xsl:call-template name="get_Task_People" />
			<xsl:text>   </xsl:text>
			<xsl:call-template name="get_Task_Base_Info" />
			<xsl:text>   </xsl:text>
			<xsl:call-template name="get_Task_Fileref" />
			<xsl:text>   </xsl:text>
			<xsl:call-template name="get_Task_Comment" />			
		</li>
	</xsl:template>
	<!--
	Get the title of the current task
	-->
	<xsl:template name="get_MasterTask_Title">		
		<xsl:element name="span">
			<xsl:choose>
				<xsl:when test="../../../TODOLIST">
					<xsl:choose>
						<xsl:when test="@DONEDATESTRING"><xsl:attribute name="class">masterCompletedTaskTitleText</xsl:attribute></xsl:when>
						<xsl:otherwise><xsl:attribute name="class">masterTaskTitleText</xsl:attribute></xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:when test="../../TODOLIST">
					<xsl:choose>
						<xsl:when test="@DONEDATESTRING"><xsl:attribute name="class">masterCompletedTaskTitleText</xsl:attribute></xsl:when>
						<xsl:otherwise><xsl:attribute name="class">masterTaskTitleText</xsl:attribute></xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>				
					<xsl:choose>
						<xsl:when test="@DONEDATESTRING"><xsl:attribute name="class">masterCompletedTaskTitleText2</xsl:attribute></xsl:when>
						<xsl:otherwise><xsl:attribute name="class">masterTaskTitleText2</xsl:attribute></xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:element name="a">
				<xsl:value-of select="@TITLE" />
			</xsl:element>
		</xsl:element>
	</xsl:template>	
	<xsl:template name="get_Task_Title">		
		<xsl:element name="span">
			<xsl:choose>
				<xsl:when test="@DONEDATESTRING"><xsl:attribute name="class">subCompletedTaskTitleText</xsl:attribute></xsl:when>
				<xsl:otherwise><xsl:attribute name="class">subTaskTitleText</xsl:attribute></xsl:otherwise>				
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="@TEXTWEBCOLOR"><xsl:attribute name="style">color: <xsl:value-of select="@TEXTWEBCOLOR" /></xsl:attribute></xsl:when>
				<xsl:otherwise></xsl:otherwise>				
			</xsl:choose>
			<xsl:element name="a">
				<xsl:value-of select="@TITLE" />
			</xsl:element>
		</xsl:element>
	</xsl:template>	
	<!--
	Retrieves summary information for task
	-->
	<xsl:template name="get_Task_Base_Info">
		<xsl:element name="span">
			<!-- choose style to use based on completion -->
			<xsl:choose>
				<xsl:when test="@DONEDATESTRING"><xsl:attribute name="class">completedBaseInfoText</xsl:attribute></xsl:when>
				<xsl:otherwise><xsl:attribute name="class">baseInfoText</xsl:attribute></xsl:otherwise>
			</xsl:choose>			
			<!-- Status -->
			<xsl:if test="@STATUS">
				<xsl:text> </xsl:text><xsl:element name="a">[Status: <xsl:value-of select="@STATUS" />]</xsl:element>
			</xsl:if>
			<!-- Category -->
			<xsl:if test="@CATEGORY">
				<xsl:text> </xsl:text><xsl:element name="a">[Category : <xsl:value-of select="@CATEGORY" />
				<xsl:if test="@CATEGORY1">;<xsl:value-of select="@CATEGORY1" /></xsl:if>
				<xsl:if test="@CATEGORY2">;<xsl:value-of select="@CATEGORY2" /></xsl:if>
				<xsl:if test="@CATEGORY3">;<xsl:value-of select="@CATEGORY3" /></xsl:if>
				<xsl:if test="@CATEGORY4">;<xsl:value-of select="@CATEGORY4" /></xsl:if>
				<xsl:if test="@CATEGORY5">;<xsl:value-of select="@CATEGORY5" /></xsl:if>
				<xsl:if test="@CATEGORY6">;<xsl:value-of select="@CATEGORY6" /></xsl:if>
				<xsl:if test="@CATEGORY7">;<xsl:value-of select="@CATEGORY7" /></xsl:if>
				<xsl:if test="@CATEGORY8">;<xsl:value-of select="@CATEGORY8" /></xsl:if>					
				<xsl:if test="@CATEGORY9">;<xsl:value-of select="@CATEGORY9" /></xsl:if>]</xsl:element>
			</xsl:if>				
			<!-- elements that always exist -->
			<xsl:text> </xsl:text><xsl:element name="a">[Task ID: <xsl:value-of select="@ID" />]</xsl:element>
			<xsl:text> </xsl:text><xsl:element name="a"><xsl:call-template name="get_Task_Dependency" /></xsl:element>
			<xsl:text> </xsl:text><xsl:element name="a"><xsl:call-template name="get_Pretty_Priority" /></xsl:element>			
			<xsl:text> </xsl:text><xsl:element name="a"><xsl:call-template name="get_Pretty_Risk" /></xsl:element>			
			<xsl:text> </xsl:text><xsl:element name="a"><xsl:call-template name="get_Pretty_Percent_Bar" /></xsl:element>
		</xsl:element>
	</xsl:template>	
	<!--
	Gets a visual percent bar
	-->
	<xsl:template name="get_Pretty_Percent_Bar">		
		<!-- only add percent bar if not 100 or 0 percent -->
		<xsl:if test="@CALCPERCENTDONE&lt;100">
			<xsl:if test="@CALCPERCENTDONE&gt;0">
				<xsl:element name="div">
					<xsl:attribute name="class">progressBarBorder</xsl:attribute>
					<xsl:attribute name="style">display: inline</xsl:attribute>
					<xsl:element name="div">
						<xsl:attribute name="class">progressBar</xsl:attribute>
						<xsl:attribute name="style">width: <xsl:value-of select="@CALCPERCENTDONE" />%</xsl:attribute>						
					</xsl:element>
				</xsl:element>					
			</xsl:if>
		</xsl:if>
		<xsl:text> </xsl:text>
		<xsl:element name="span">
			<xsl:choose>
				<xsl:when test="@CALCPERCENTDONE">[ <xsl:value-of select="@CALCPERCENTDONE" />% ]</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:element>
	</xsl:template>	
	<!--
	Priority
	-->
	<xsl:template name="get_Pretty_Priority">
		<xsl:element name="span">
			<xsl:element name="span"><xsl:text> </xsl:text>[Priority: </xsl:element>
			<xsl:element name="span">
				<!--<xsl:attribute name="class">prettyPriority</xsl:attribute>-->
				<xsl:choose>
					<xsl:when test="@PRIORITY&gt;-1">
						<xsl:choose>
							<xsl:when test="@DONEDATESTRING">								
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="class">priorityNumberStyle</xsl:attribute>															
								<xsl:attribute name="style">background-color: <xsl:value-of select="@PRIORITYWEBCOLOR" /></xsl:attribute>
							</xsl:otherwise>							
						</xsl:choose>						
						<xsl:element name="a">
							<!-- only non-finished task get coloured priorities -->
							<xsl:value-of select="@PRIORITY" />
						</xsl:element>
					</xsl:when>
					<xsl:otherwise>
						<xsl:element name="a">
							<xsl:attribute name="class">priorityNumberStyle</xsl:attribute>
							<xsl:text></xsl:text>
						</xsl:element>				
					</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
			<xsl:element name="span">]</xsl:element>
		</xsl:element>
	</xsl:template>
	<!--
	Risk
	-->
	<xsl:template name="get_Pretty_Risk">
		<xsl:element name="span">
			<xsl:element name="span"><xsl:text> </xsl:text>[Risk: </xsl:element>
			<xsl:element name="span">
				<!--<xsl:attribute name="class">prettyRisk</xsl:attribute>-->
				<xsl:choose>
					<xsl:when test="@RISK&gt;-1">
						<xsl:choose>
							<xsl:when test="@DONEDATESTRING">								
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="class">riskNumberStyle</xsl:attribute>																							
							</xsl:otherwise>							
						</xsl:choose>						
						<xsl:element name="a">
							<!-- only non-finished task get coloured priorities -->
							<xsl:value-of select="@RISK" />
						</xsl:element>
					</xsl:when>
					<xsl:otherwise>
						<xsl:element name="a">
							<xsl:attribute name="class">riskNumberStyle</xsl:attribute>
							<xsl:text></xsl:text>
						</xsl:element>				
					</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
			<xsl:element name="span">]</xsl:element>
		</xsl:element>
	</xsl:template>
	<!--	gets task dependendies	-->
	<xsl:template name="get_Task_Dependency">		
		<xsl:if test="@DEPENDS">
			<xsl:text> </xsl:text><xsl:element name="a">[Next : <xsl:value-of select="@DEPENDS" />
			<xsl:if test="@DEPENDS1">;<xsl:value-of select="@DEPENDS1" /></xsl:if>
			<xsl:if test="@DEPENDS2">;<xsl:value-of select="@DEPENDS2" /></xsl:if>
			<xsl:if test="@DEPENDS3">;<xsl:value-of select="@DEPENDS3" /></xsl:if>
			<xsl:if test="@DEPENDS4">;<xsl:value-of select="@DEPENDS4" /></xsl:if>
			<xsl:if test="@DEPENDS5">;<xsl:value-of select="@DEPENDS5" /></xsl:if>
			<xsl:if test="@DEPENDS6">;<xsl:value-of select="@DEPENDS6" /></xsl:if>
			<xsl:if test="@DEPENDS7">;<xsl:value-of select="@DEPENDS7" /></xsl:if>
			<xsl:if test="@DEPENDS8">;<xsl:value-of select="@DEPENDS8" /></xsl:if>					
			<xsl:if test="@DEPENDS9">;<xsl:value-of select="@DEPENDS9" /></xsl:if>]</xsl:element>
		</xsl:if>
	</xsl:template>
	<!--
	gets task dates
	-->
	<xsl:template name="get_Task_Dates">
		<xsl:element name="span">
			<!-- choose style to use based on completion -->
			<xsl:choose>
				<xsl:when test="@DONEDATESTRING"><xsl:attribute name="class">completedDatesText</xsl:attribute></xsl:when>
				<xsl:otherwise><xsl:attribute name="class">datesText</xsl:attribute></xsl:otherwise>
			</xsl:choose>				
			<!-- Start date -->
			<xsl:if test="@STARTDATESTRING">
				<xsl:element name="a"><xsl:text> </xsl:text>[Start: <xsl:value-of select="@STARTDATESTRING" />]</xsl:element>
			</xsl:if>
			<!-- due date -->
			<xsl:choose>
				<xsl:when test="@DUEDATESTRING">
					<xsl:element name="a"><xsl:text> </xsl:text>[Due: <xsl:value-of select="@DUEDATESTRING" />]</xsl:element>
				</xsl:when>
				<xsl:otherwise>
					<xsl:element name="a"><xsl:text> </xsl:text>[Due: Not Set]</xsl:element>
				</xsl:otherwise>
			</xsl:choose>						
		</xsl:element>
	</xsl:template>	
	<!--
	get people allocated to/by 
	-->
	<xsl:template name="get_Task_People">
		<xsl:element name="span">
			<xsl:choose>
				<xsl:when test="@DONEDATESTRING"><xsl:attribute name="class">completedPeopleText</xsl:attribute></xsl:when>
				<xsl:otherwise><xsl:attribute name="class">peopleText</xsl:attribute></xsl:otherwise>
			</xsl:choose>			
			<!-- allocated to -->
			<xsl:choose>
				<xsl:when test="@PERSON">
					<xsl:text> </xsl:text><xsl:element name="a">[Allocated to: <xsl:value-of select="@PERSON" />
					<xsl:if test="@PERSON1">;<xsl:value-of select="@PERSON1" /></xsl:if>
					<xsl:if test="@PERSON2">;<xsl:value-of select="@PERSON2" /></xsl:if>
					<xsl:if test="@PERSON3">;<xsl:value-of select="@PERSON3" /></xsl:if>
					<xsl:if test="@PERSON4">;<xsl:value-of select="@PERSON4" /></xsl:if>
					<xsl:if test="@PERSON5">;<xsl:value-of select="@PERSON5" /></xsl:if>
					<xsl:if test="@PERSON6">;<xsl:value-of select="@PERSON6" /></xsl:if>
					<xsl:if test="@PERSON7">;<xsl:value-of select="@PERSON7" /></xsl:if>
					<xsl:if test="@PERSON8">;<xsl:value-of select="@PERSON8" /></xsl:if>					
					<xsl:if test="@PERSON9">;<xsl:value-of select="@PERSON9" /></xsl:if>]</xsl:element>
				</xsl:when>
			</xsl:choose>			
			<!-- allocated by -->
			<xsl:choose>
				<xsl:when test="@ALLOCATEDBY">
					<xsl:text> </xsl:text><xsl:element name="a">[Allocated by: <xsl:value-of select="@ALLOCATEDBY" />]</xsl:element>
				</xsl:when>
			</xsl:choose>
		</xsl:element>
	</xsl:template>	
	<!-- Fileref -->	
	<xsl:template name="get_Task_Fileref">
		<xsl:element name="span">
			<xsl:choose>
				<xsl:when test="@DONEDATESTRING"><xsl:attribute name="class">completedFilerefpathText</xsl:attribute></xsl:when>
				<xsl:otherwise><xsl:attribute name="class">filerefpathText</xsl:attribute></xsl:otherwise>
			</xsl:choose>					
			<xsl:choose>
				<xsl:when test="@FILEREFPATH">
				  <xsl:text> </xsl:text>[Fileref: 
					<xsl:element name="a"><xsl:attribute name="href"><xsl:value-of select="@FILEREFPATH" /></xsl:attribute>><xsl:value-of select="@FILEREFPATH" /></xsl:element>
					]
				</xsl:when>
			</xsl:choose>
	</xsl:element>
	</xsl:template>
	<!-- Comments -->
	<xsl:template name="get_Task_Comment">
		<xsl:element name="span">
			<xsl:choose>
				<xsl:when test="@DONEDATESTRING"><xsl:attribute name="class">completedCommentsText</xsl:attribute></xsl:when>
				<xsl:otherwise><xsl:attribute name="class">commentsText</xsl:attribute></xsl:otherwise>
			</xsl:choose>
			<xsl:if test="@COMMENTS">
				<xsl:element name="br"/>
				<xsl:element name="a"><xsl:value-of select="@COMMENTS" /></xsl:element>
			</xsl:if>
    </xsl:element>
	</xsl:template>
</xsl:stylesheet>

