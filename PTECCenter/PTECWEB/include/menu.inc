

<!-- Sidebar -->

  <div class="collapse navbar-collapse" id="navbarSupportedContent" style="flex-basis: auto;">
    <ul class="sidebar navbar-nav h-100">
      <li class="nav-item active">
        <a class="nav-link" href="<%=Page.ResolveUrl("~/default.aspx")%>">
          <i class="fas fa-fw fa-tachometer-alt"></i>
          <span>Home</span>
        </a>
      </li>
	  <!-- loop menu parent_id=1 -->
	  <% for i=0 to menutable.rows.count -1 %>
			<% if menutable.rows(i).item("parent_id") = 1 then 
				if menutable.rows(i).item("parent") = 1 %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>

			<% if menutable.rows(i).item("parent_id") = 2 then 
				if menutable.rows(i).item("parent") = 1 %>
					<% if i>0 then %> </div></li> <!--ปิด tag menu ก่อนหน้า -->  <% end if %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>

			<% if menutable.rows(i).item("parent_id") = 3 then 
				if menutable.rows(i).item("parent") = 1 %>
					<% if i>0 then %> </div></li> <!--ปิด tag menu ก่อนหน้า -->  <% end if %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>

			<% if menutable.rows(i).item("parent_id") = 4 then 
				if menutable.rows(i).item("parent") = 1 %>
					<% if i>0 then %> </div></li> <!--ปิด tag menu ก่อนหน้า -->  <% end if %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>

			<% if menutable.rows(i).item("parent_id") = 5 then 
				if menutable.rows(i).item("parent") = 1 %>
					<% if i>0 then %> </div></li> <!--ปิด tag menu ก่อนหน้า -->  <% end if %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>

			<% if menutable.rows(i).item("parent_id") = 6 then 
				if menutable.rows(i).item("parent") = 1 %>
					<% if i>0 then %> </div></li> <!--ปิด tag menu ก่อนหน้า -->  <% end if %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>

			<% if menutable.rows(i).item("parent_id") = 7 then 
				if menutable.rows(i).item("parent") = 1 %>
					<% if i>0 then %> </div></li> <!--ปิด tag menu ก่อนหน้า -->  <% end if %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>

			<% if menutable.rows(i).item("parent_id") = 8 then 
				if menutable.rows(i).item("parent") = 1 %>
					<% if i>0 then %> </div></li> <!--ปิด tag menu ก่อนหน้า -->  <% end if %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>

			<% if menutable.rows(i).item("parent_id") = 9 then 
				if menutable.rows(i).item("parent") = 1 %>
					<% if i>0 then %> </div></li> <!--ปิด tag menu ก่อนหน้า -->  <% end if %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>
			<% if menutable.rows(i).item("parent_id") = 10 then 
				if menutable.rows(i).item("parent") = 1 %>
					<% if i>0 then %> </div></li> <!--ปิด tag menu ก่อนหน้า -->  <% end if %>
				  <li class="nav-item dropdown">
					<a class="nav-link dropdown-toggle" href="#" id="pagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
					  <i class="fas fa-fw fa-folder"></i>
					  <span><% =menutable.rows(i).item("menu_name") %></span>
					</a>
					<div class="dropdown-menu" aria-labelledby="pagesDropdown">
				<% else %>			
					<% if menutable.rows(i).item("menu_name")="-" then %>
						<div class="dropdown-divider"></div>
					<% else %>
						<div style="font-size:12px"><a class="dropdown-item" href="<%=Page.ResolveUrl(menutable.rows(i).item("menu_url"))%>"><div class="row flex-wrap"><div class="text-left col"><% =menutable.rows(i).item("menu_name") %></div><div class="text-right col"><% if menutable.rows(i).item("active_mobile")="1" then %><i class="fas fa-mobile-alt mr-1" style="font-size: 10px;color: #d2d2d2;"></i><% end if %><% if menutable.rows(i).item("active_pc")="1" then %><i class="fas fa-desktop" style="font-size: 10px;color: #d2d2d2;"></i><% end if %></div></div></a></div>
					<% end if %>
				<% end if 
			end if %>
	  <%
		 next i
	  %>
			</div>
		  </li>


	  <!-- end loop menu parent_id=1 -->

    </ul>

    <!-- end Sidebar -->
	  </div>